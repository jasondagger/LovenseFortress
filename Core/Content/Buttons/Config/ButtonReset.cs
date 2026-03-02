
using Godot;
using LovenseFortress.Core.Services.Configs;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons.Config;

public sealed partial class ButtonReset() : 
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

	[Export] private ButtonDeathVibrationIntensityCritical ButtonDeathVibrationIntensityCritical = null;
	[Export] private ButtonDeathVibrationIntensityNormal   ButtonDeathVibrationIntensityNormal   = null;
	[Export] private ButtonEchoVibrationIntensity          ButtonEchoVibrationIntensity          = null;
	[Export] private ButtonEchoVibrationLength             ButtonEchoVibrationLength             = null;
	[Export] private ButtonKillStreakVibrationLength0      ButtonKillStreakVibrationLength0      = null;
	[Export] private ButtonKillStreakVibrationLength5      ButtonKillStreakVibrationLength5      = null;
	[Export] private ButtonKillStreakVibrationLength10     ButtonKillStreakVibrationLength10     = null;
	[Export] private ButtonKillStreakVibrationLength15     ButtonKillStreakVibrationLength15     = null;
	[Export] private ButtonKillStreakVibrationLength20     ButtonKillStreakVibrationLength20     = null;
	[Export] private ButtonKillStreakVibrationLength25     ButtonKillStreakVibrationLength25     = null;
	[Export] private ButtonKillVibrationIntensityCritical  ButtonKillVibrationIntensityCritical  = null;
	[Export] private ButtonKillVibrationIntensityNormal    ButtonKillVibrationIntensityNormal    = null;
	[Export] private ButtonSuicideVibrationIntensity       ButtonSuicideVibrationIntensity       = null;
	
	private void OnPressed()
	{
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.DeathVibrationIntensityCritical,
			value:          ServiceTeamFortress2Constants.DeathVibrationIntensityCritical.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.DeathVibrationIntensityNormal,
			value:          ServiceTeamFortress2Constants.DeathVibrationIntensityNormal.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.EchoVibrationIntensity,
			value:          ServiceTeamFortress2Constants.EchoVibrationIntensity.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.EchoVibrationLength,
			value:          ServiceTeamFortress2Constants.EchoVibrationLength.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength0,
			value:          ServiceTeamFortress2Constants.KillStreakVibrationLength0.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength5,
			value:          ServiceTeamFortress2Constants.KillStreakVibrationLength5.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength10,
			value:          ServiceTeamFortress2Constants.KillStreakVibrationLength10.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength15,
			value:          ServiceTeamFortress2Constants.KillStreakVibrationLength15.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength20,
			value:          ServiceTeamFortress2Constants.KillStreakVibrationLength20.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillStreakVibrationLength25,
			value:          ServiceTeamFortress2Constants.KillStreakVibrationLength25.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillVibrationIntensityCritical,
			value:          ServiceTeamFortress2Constants.KillVibrationIntensityCritical.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.KillVibrationIntensityNormal,
			value:          ServiceTeamFortress2Constants.KillVibrationIntensityNormal.ToString()
		);
		
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.SuicideVibrationIntensity,
			value:          ServiceTeamFortress2Constants.SuicideVibrationIntensity.ToString()
		);

		this.ButtonDeathVibrationIntensityCritical.UpdateValue();
		this.ButtonDeathVibrationIntensityNormal.UpdateValue();
		this.ButtonEchoVibrationIntensity.UpdateValue();
		this.ButtonEchoVibrationLength.UpdateValue(); 
		this.ButtonKillStreakVibrationLength0.UpdateValue();
		this.ButtonKillStreakVibrationLength5.UpdateValue();
		this.ButtonKillStreakVibrationLength10.UpdateValue();
		this.ButtonKillStreakVibrationLength15.UpdateValue();
		this.ButtonKillStreakVibrationLength20.UpdateValue();
		this.ButtonKillStreakVibrationLength25.UpdateValue();
		this.ButtonKillVibrationIntensityCritical.UpdateValue();
		this.ButtonKillVibrationIntensityNormal.UpdateValue();
		this.ButtonSuicideVibrationIntensity.UpdateValue();
	}
}