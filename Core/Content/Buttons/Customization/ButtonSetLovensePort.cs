
using Godot;
using LovenseFortress.Core.Services.Configs;

namespace LovenseFortress.Core.Content.Buttons.Customization;

public sealed partial class ButtonSetLovensePort() : 
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
		var lovensePort = serviceConfig.GetLovensePort();
		
		this.TextEditLovensePort.Text = lovensePort; 
	}
	
	[Export] private TextEdit TextEditLovensePort = null;

	private void OnPressed()
	{
		var serviceConfig = Services.Services.GetService<ServiceConfig>();
		serviceConfig.UpdateConfigFile(
			configConstant: ServiceConfigConstants.LovensePort,
			value:          this.TextEditLovensePort.Text
		);
	}
}