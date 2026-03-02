
using Godot;
using LovenseFortress.Core.Services.Configs;

namespace LovenseFortress.Core.Content.Buttons.Customization;

public sealed partial class ButtonSetLovenseLocalIP() : 
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
		var lovenseIP = serviceConfig.GetLovenseIP();
		
		this.TextEditLovenseLocalIP.Text = lovenseIP; 
	}
	
	[Export] private TextEdit TextEditLovenseLocalIP = null;
	
	private void OnPressed()
	{
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.LovenseLocalIP,
			value:          this.TextEditLovenseLocalIP.Text
		);
	}
}