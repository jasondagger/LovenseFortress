
using Godot;

namespace LovenseFortress.Core.Content.Buttons;

public partial class ButtonClose() :
	Button()
{
	public override void _EnterTree()
	{
		this.Pressed += this.Close;
	}

	public override void _ExitTree()
	{
		this.Pressed -= this.Close;
	}

	private void Close()
	{
		var tree = this.GetTree();
		tree.Quit();
	}
}