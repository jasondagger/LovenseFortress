
using Godot;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons;

public partial class ButtonStart() :
	Button()
{
	public override void _EnterTree()
	{
		this.Pressed += ButtonStart.ConnectToTeamFortress2;
	}

	public override void _ExitTree()
	{
		this.Pressed -= ButtonStart.ConnectToTeamFortress2;
	}

	private static void ConnectToTeamFortress2()
	{
		var serviceTeamFortress2 = Services.Services.GetService<ServiceTeamFortress2>();
		serviceTeamFortress2.StartReadingConsoleLog();
	}
}