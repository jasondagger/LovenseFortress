
using Godot;
using LovenseFortress.Core.Services.Configs;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons.Config;

public sealed partial class ButtonKillStreakVibrationLength20() : 
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
		var killStreakVibrationLength20 = serviceTeamFortress2.GetKillStreakVibrationLength20();
		
		this.TextEditKillStreakVibrationLength20.Text = killStreakVibrationLength20.ToString(); 
	}
	
	[Export] private TextEdit TextEditKillStreakVibrationLength20 = null;

	private void OnPressed()
	{
		if (
			int.TryParse(
				s:      this.TextEditKillStreakVibrationLength20.Text,
				result: out var value
			) is false ||
			value <= 0
		)
		{
			this.TextEditKillStreakVibrationLength20.Text = "Value must be >= to 1.";
			return;
		}
		
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength20,
			value:          this.TextEditKillStreakVibrationLength20.Text
		);
	}
}