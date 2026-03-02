
using Godot;
using LovenseFortress.Core.Tools;

namespace LovenseFortress.Core.Services.StreamStates;

internal abstract class ServiceStreamState()
{
	protected Node Node { get; private set; } = null;

	protected void AddStreamStateSceneToStreamStatesScene(
        string filePathStreamState
    )
    {
        var packedScene = ResourceLoader.Load(
            path:      filePathStreamState,
            typeHint:  $"{nameof(PackedScene)}",
            cacheMode: ResourceLoader.CacheMode.Reuse
        ) as PackedScene;
        if (packedScene is not null)
        {
            this.Node             = packedScene.Instantiate();
			var serviceGameStates = Services.GetService<ServiceStreamStates>();
            serviceGameStates.ParentNodeToRoot(
                node: this.Node
            );
        }
        else
        {
            ConsoleLogger.LogMessageError(
                messageError:
                    $"{nameof(ServiceStreamState)}." +
                    $"{nameof(ServiceStreamState.AddStreamStateSceneToStreamStatesScene)}() - " +
                    $"EXCEPTION: Failed to load from {filePathStreamState}"
            );
        }
    }

    protected void RemoveStreamStateSceneFromMainScene()
    {
        if (this.Node is null)
        {
            return;
        }
        
        this.Node.QueueFree();
        this.Node = null;
    }
}