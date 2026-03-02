
using Godot;

namespace LovenseFortress.Core.Content;

public sealed partial class Console() :
	Control()
{
	public override void _EnterTree()
	{
		this.CacheInstance();
		this.WriteStoredLogs();
	}

	internal static void WriteLine(
		string text
	)
	{
		var appendedText = $"{text}\n";
		if (Console.s_instance is null)
		{
			Console.s_storedLogs += appendedText;
		}
		else
		{
			Console.s_instance.Log.CallDeferred(
				method: RichTextLabel.MethodName.AppendText, 
				args:   appendedText
			);
		}
	}

	
	[Export] private RichTextLabel Log                      = null;
	
	private static Console         s_instance { get; set; } = null;
	private static string          s_storedLogs             = string.Empty;

	private void CacheInstance()
	{
		Console.s_instance = this;
	}

	private void WriteStoredLogs()
	{
		this.Log.AppendText(
			bbcode: Console.s_storedLogs
		);
	}
}
