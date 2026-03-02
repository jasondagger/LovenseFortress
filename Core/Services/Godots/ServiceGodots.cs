
using Godot;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using LovenseFortress.Core.Services.Godots.Https;
using LovenseFortress.Core.Services.Godots.Inputs;
using LovenseFortress.Core.Tools;
using LovenseFortress.Core.Tools.ResourcePaths;

namespace LovenseFortress.Core.Services.Godots;

internal sealed class ServiceGodots :
	ServiceNode
{
    public override async Task Setup()
    {
        await base.Setup();

        this.AddServiceGodotScenes();
    }

    internal TServiceGodot GetServiceGodot<TServiceGodot>()
        where TServiceGodot :
            ServiceGodot
    {
        return this.m_serviceGodots[key: typeof(TServiceGodot)] as TServiceGodot;
    }

    private static readonly Dictionary<Type, string> c_serviceGodotTypePaths = new()
    {
        { typeof(ServiceGodotHttp),  ResourcePaths.GodotHttp  },
        { typeof(ServiceGodotInput), ResourcePaths.GodotInput },
	};
	private readonly Dictionary<Type, ServiceGodot>  m_serviceGodots         = new();

    private void AddServiceGodotScenes()
    {
		var type   = this.GetType();
        var method = type.GetMethod(
            name:        $"{nameof(ServiceGodots.AddServiceGodotScene)}",
            bindingAttr:
                BindingFlags.Instance |
                BindingFlags.NonPublic
		);

        if (method is null)
        {
            return;
        }

        foreach (var (serviceGodotType, serviceGodotPath) in ServiceGodots.c_serviceGodotTypePaths)
        {
            var parameters    = new object[] 
            {
                serviceGodotPath
            };
            var genericMethod = method.MakeGenericMethod(
                typeArguments: serviceGodotType
            );

            genericMethod.Invoke(
                obj:        this,
                parameters: parameters
            );
        }
    }

    private void AddServiceGodotScene<TServiceGodot>(
        string path
    )
		where TServiceGodot :
			ServiceGodot
	{
        if (
            ResourceLoader.Load(
                path:      path,
                typeHint:  $"{nameof(PackedScene)}",
                cacheMode: ResourceLoader.CacheMode.Reuse
            ) is PackedScene packedScene
        )
        {
            if (packedScene.Instantiate() is not TServiceGodot serviceGodot)
            {
                return;
            }
            
            serviceGodot.Start();

            this.m_serviceGodots[key: typeof(TServiceGodot)] = serviceGodot;

            base.Node.AddChild(
                node: serviceGodot
            );
        }
        else
        {
            ConsoleLogger.LogMessageError(
                messageError:
                    $"{nameof(ServiceGodots)}." +
                    $"{nameof(ServiceGodots.AddServiceGodotScene)}() - " +
                    $"Failed to load from {path}"
            );
        }
    }
}