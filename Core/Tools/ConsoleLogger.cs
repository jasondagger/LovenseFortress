
namespace LovenseFortress.Core.Tools;

using Godot;
using System;

internal static class ConsoleLogger
{
    internal static void LogMessage(
        string message    
    )
    {
        var timeStampSystem = ConsoleLogger.GetTimeStampSystem();
        var fullMessage     = $"{timeStampSystem} {message}";
        
        lock (ConsoleLogger.s_lock)
        {
            GD.Print(
                what: fullMessage
            );
        }
    }

    internal static void LogMessageError(
        string messageError    
    )
    {
        var timeStampSystem = ConsoleLogger.GetTimeStampSystem();
        var fullMessage     = $"{timeStampSystem} {messageError}";
        lock (ConsoleLogger.s_lock)
        {
            GD.PrintErr(
                what: fullMessage
            );
        }
    }

    private static readonly object s_lock = new();

    private static string GetTimeStampSystem()
    {
        var dateTime = DateTime.Now;
        return $"[{dateTime:yyyy-MM-dd} {dateTime:HH:mm:ss.fff}]";
    }
}