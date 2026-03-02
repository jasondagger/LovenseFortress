
using Godot;
using LovenseFortress.Core.Services.Configs;

namespace LovenseFortress.Core.Content.Buttons.Customization;

public sealed partial class ButtonSetTeamFortress2Path() : 
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
		var teamFortress2Path = serviceConfig.GetTeamFortress2Path();
		
		this.TextEditTeamFortress2Path.Text = teamFortress2Path; 
	}
	
	[Export] private TextEdit TextEditTeamFortress2Path = null;
	
	private void OnPressed()
	{
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.TeamFortress2Path,
			value:          this.TextEditTeamFortress2Path.Text
		);
	}
}