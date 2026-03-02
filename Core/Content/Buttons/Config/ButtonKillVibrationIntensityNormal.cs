
using Godot;
using LovenseFortress.Core.Services.Configs;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons.Config;

public sealed partial class ButtonKillVibrationIntensityNormal() : 
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
		var serviceTeamFortress2         = Services.Services.GetService<ServiceTeamFortress2>();
		var killVibrationIntensityNormal = serviceTeamFortress2.GetKillVibrationIntensityNormal();
		
		this.TextEditKillVibrationIntensityNormal.Text = killVibrationIntensityNormal.ToString(); 
	}
	
	[Export] private TextEdit TextEditKillVibrationIntensityNormal = null;
	
	private void OnPressed()
	{
		if (
			int.TryParse(
				s:      this.TextEditKillVibrationIntensityNormal.Text,
				result: out var value
			) is false ||
			value is <= 0 or > 20
		)
		{
			this.TextEditKillVibrationIntensityNormal.Text = "Value must be between 1 and 20.";
			return;
		}
		
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillVibrationIntensityNormal,
			value:          this.TextEditKillVibrationIntensityNormal.Text
		);
	}
}