
using Godot;
using LovenseFortress.Core.Services.Configs;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons.Config;

public sealed partial class ButtonKillStreakVibrationLength25() : 
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
		var serviceTeamFortress2        = Services.Services.GetService<ServiceTeamFortress2>();
		var killStreakVibrationLength25 = serviceTeamFortress2.GetKillStreakVibrationLength25();
		
		this.TextEditKillStreakVibrationLength25.Text = killStreakVibrationLength25.ToString(); 
	}
	
	[Export] private TextEdit TextEditKillStreakVibrationLength25 = null;

	private void OnPressed()
	{
		if (
			int.TryParse(
				s:      this.TextEditKillStreakVibrationLength25.Text,
				result: out var value
			) is false ||
			value <= 0
		)
		{
			this.TextEditKillStreakVibrationLength25.Text = "Value must be >= to 1.";
			return;
		}
		
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength25,
			value:          this.TextEditKillStreakVibrationLength25.Text
		);
	}
}