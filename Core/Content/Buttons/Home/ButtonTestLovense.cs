
using Godot;
using LovenseFortress.Core.Services.Lovenses;

namespace LovenseFortress.Core.Content.Buttons;

public partial class ButtonTestLovense() :
    Button()
{
    public override void _EnterTree()
    {
        this.Pressed += ButtonTestLovense.TestLovense;
    }

    public override void _ExitTree()
    {
        this.Pressed -= ButtonTestLovense.TestLovense;
    }

    private static void TestLovense()
    {
        var serviceLovense = Services.Services.GetService<ServiceLovense>();
        serviceLovense.All(
            intensity:     20,
            timeInSeconds: 1
        );
    }
}