
using Godot;
using LovenseFortress.Core.Services.Configs;

namespace LovenseFortress.Core.Content.Buttons.Customization;

public sealed partial class ButtonSetSteamDisplayName() : 
	Button()
{
	public override void _EnterTree()
	{
		this.Pressed += this.OnPressed;
	}

	public override void _ExitTree()
	{
		this.Pressed -= this.OnPressed;
	}

	public override void _Ready()
	{
		var serviceConfig    = Services.Services.GetService<ServiceConfig>();
		var steamDisplayName = serviceConfig.GetSteamDisplayName();
		
		this.TextEditSteamDisplayName.Text = steamDisplayName; 
	}
	
	[Export] private TextEdit TextEditSteamDisplayName = null;
	
	private void OnPressed()
	{
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.SteamDisplayName,
			value:          this.TextEditSteamDisplayName.Text
		);
	}
}