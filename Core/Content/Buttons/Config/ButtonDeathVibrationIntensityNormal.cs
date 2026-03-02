using Godot;
using LovenseFortress.Core.Services.Configs;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons.Config;

public sealed partial class ButtonDeathVibrationIntensityNormal() : 
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
		this.UpdateValue();
	}

	internal void UpdateValue()
	{
		var serviceTeamFortress2 = Services.Services.GetService<ServiceTeamFortress2>();
		var deathVibrationIntensityNormal = serviceTeamFortress2.GetDeathVibrationIntensityNormal();
		
		this.TextEditDeathVibrationIntensityNormal.Text = deathVibrationIntensityNormal.ToString(); 
	}
	
	[Export] private TextEdit TextEditDeathVibrationIntensityNormal = null;
	
	private void OnPressed()
	{
		if (
			int.TryParse(
				s:      this.TextEditDeathVibrationIntensityNormal.Text,
				result: out var value
			) is false ||
			value is <= 0 or > 20)
		{
			this.TextEditDeathVibrationIntensityNormal.Text = "Value must be between 1 and 20.";
			return;
		}
		
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.DeathVibrationIntensityNormal,
			value:          this.TextEditDeathVibrationIntensityNormal.Text
		);
	}
}