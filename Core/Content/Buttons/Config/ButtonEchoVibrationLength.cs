using Godot;
using LovenseFortress.Core.Services.Configs;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons.Config;

public sealed partial class ButtonEchoVibrationLength() : 
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
		var echoVibrationLength = serviceTeamFortress2.GetEchoVibrationLength();
		
		this.TextEditEchoVibrationLength.Text = echoVibrationLength.ToString(); 
	}
	
	[Export] private TextEdit TextEditEchoVibrationLength = null;
	
	private void OnPressed()
	{
		if (
			int.TryParse(
				s:      this.TextEditEchoVibrationLength.Text,
				result: out var value
			) is false ||
			value <= 0
		)
		{
			this.TextEditEchoVibrationLength.Text = "Value must be >= to 1.";
			return;
		}
		
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.EchoVibrationLength,
			value:          this.TextEditEchoVibrationLength.Text
		);
	}
}