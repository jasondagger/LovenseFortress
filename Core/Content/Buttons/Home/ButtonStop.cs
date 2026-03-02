
using Godot;
using LovenseFortress.Core.Services.TeamFortress2s;

namespace LovenseFortress.Core.Content.Buttons;

public partial class ButtonStop() :
	Button()
{
	public override void _EnterTree()
	{
		this.Pressed += ButtonStop.DisconnectFromTeamFortress2;
	}

	public override void _ExitTree()
	{
		this.Pressed -= ButtonStop.DisconnectFromTeamFortress2;
	}

	private static void DisconnectFromTeamFortress2()
	{
		var serviceTeamFortress2 = Services.Services.GetService<ServiceTeamFortress2>();
		serviceTeamFortress2.StopReadingConsoleLog();
	}
}