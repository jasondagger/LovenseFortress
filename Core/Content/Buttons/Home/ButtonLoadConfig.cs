
using Godot;
using LovenseFortress.Core.Services.Configs;
using System.Threading.Tasks;

namespace LovenseFortress.Core.Content.Buttons.Home;

public partial class ButtonLoadConfig() :
	Button()
{
	public override void _EnterTree()
	{
		this.Pressed += ButtonLoadConfig.LoadConfig;
	}

	public override void _ExitTree()
	{
		this.Pressed -= ButtonLoadConfig.LoadConfig;
	}

	private static void LoadConfig()
	{
		Task.Run(
			function: async () =>
			{
				var serviceConfig = Services.Services.GetService<ServiceConfig>();
				await serviceConfig.LoadConfigFile();
			}
		);
	}
}