
using Godot;
using LovenseFortress.Core.Services.Configs;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons.Config;

public sealed partial class ButtonSuicideVibrationIntensity() : 
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
		var serviceTeamFortress2      = Services.Services.GetService<ServiceTeamFortress2>();
		var suicideVibrationIntensity = serviceTeamFortress2.GetSuicideVibrationIntensity();
		
		this.TextEditSuicideVibrationIntensity.Text = suicideVibrationIntensity.ToString(); 
	}
	
	[Export] private TextEdit TextEditSuicideVibrationIntensity = null;
	
	private void OnPressed()
	{
		if (
			int.TryParse(
				s:      this.TextEditSuicideVibrationIntensity.Text,
				result: out var value
			) is false ||
			value is <= 0 or > 20
		)
		{
			this.TextEditSuicideVibrationIntensity.Text = "Value must be between 1 and 20.";
			return;
		}
		
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.SuicideVibrationIntensity,
			value:          this.TextEditSuicideVibrationIntensity.Text
		);
	}
}