
using Godot;

namespace LovenseFortress.Core.Content.Buttons.Main;

public partial class ButtonCustomization() :
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
	
	[Export] private ScreenController ScreenController = null;

	private void OnPressed()
	{
		this.ScreenController.OpenPageCustomization();
	}
}