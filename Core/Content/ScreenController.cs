using Godot;

namespace LovenseFortress.Core.Content;

public partial class ScreenController() :
	Node()
{
	internal void OpenPageConfig()
	{
		this.PageConfig.Visible        = true;
		this.PageCustomization.Visible = false;
		this.PageHome.Visible          = false;
	}
	
	internal void OpenPageCustomization()
	{
		this.PageConfig.Visible        = false;
		this.PageCustomization.Visible = true;
		this.PageHome.Visible          = false;
	}
	
	internal void OpenPageHome()
	{
		this.PageConfig.Visible        = false;
		this.PageCustomization.Visible = false;
		this.PageHome.Visible          = true;
	}
	
	[Export] private Control PageConfig        = null;
	[Export] private Control PageCustomization = null;
	[Export] private Control PageHome          = null;
}