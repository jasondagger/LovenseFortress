
using System;
using Godot;
using LovenseFortress.Core.Services.Lovenses;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Console = LovenseFortress.Core.Content.Console;

namespace LovenseFortress.Core.Services.TeamFortress2s;

public sealed partial class ServiceTeamFortress2() : 
	IService
{
	Task IService.Setup()
	{
		return Task.CompletedTask;
	}
	
	Task IService.Start()
	{
		return Task.CompletedTask;
	}
	
	Task IService.Stop()
	{
		this.StopReadingConsoleLog();
		return Task.CompletedTask;
	}
	
	internal int GetDeathVibrationIntensityCritical()
	{
		return this.m_deathVibrationIntensityCritical;
	}
	
	internal int GetDeathVibrationIntensityNormal()
	{
		return this.m_deathVibrationIntensityNormal;
	}
	
	internal int GetEchoVibrationIntensity()
	{
		return this.m_echoVibrationIntensity;
	}
	
	internal int GetEchoVibrationLength()
	{
		return this.m_echoVibrationLength;
	}

	internal int GetKillStreakVibrationLength0()
	{
		return this.m_killStreakVibrationLength0;
	}
	
	internal int GetKillStreakVibrationLength5()
	{
		return this.m_killStreakVibrationLength5;
	}
	
	internal int GetKillStreakVibrationLength10()
	{
		return this.m_killStreakVibrationLength10;
	}
	
	internal int GetKillStreakVibrationLength15()
	{
		return this.m_killStreakVibrationLength15;
	}
	
	internal int GetKillStreakVibrationLength20()
	{
		return this.m_killStreakVibrationLength20;
	}
	
	internal int GetKillStreakVibrationLength25()
	{
		return this.m_killStreakVibrationLength25;
	}
	
	internal int GetKillVibrationIntensityCritical()
	{
		return this.m_killVibrationIntensityCritical;
	}
	
	internal int GetKillVibrationIntensityNormal()
	{
		return this.m_killVibrationIntensityNormal;
	}
	
	internal int GetSuicideVibrationIntensity()
	{
		return this.m_suicideVibrationIntensity;
	}
	
	internal void SetTFPath(
		string tfPath
	)
	{
		Console.WriteLine(
			text: $"Set Team Fortress 2 /tf/ path to {tfPath}."
		);
		this.m_tfPath = tfPath;
	}

	internal void SetDeathVibrationIntensityCritical(
		int intensity
	)
	{
		Console.WriteLine(
			text: $"Set Death Vibration Intensity Critical to {intensity}."
		);
		this.m_deathVibrationIntensityCritical = intensity;
	}
	
	internal void SetDeathVibrationIntensityNormal(
		int intensity
	)
	{
		Console.WriteLine(
			text: $"Set Death Vibration Intensity Normal to {intensity}."
		);
		this.m_deathVibrationIntensityNormal = intensity;
	}
	
	internal void SetEchoVibrationIntensity(
		int intensity
	)
	{
		Console.WriteLine(
			text: $"Set Echo Vibration Intensity to {intensity}."
		);
		this.m_echoVibrationIntensity = intensity;
	}
	
	internal void SetEchoVibrationLength(
		int lengthInSeconds
	)
	{
		Console.WriteLine(
			text: $"Set Echo Vibration Length to {lengthInSeconds} seconds."
		);
		this.m_echoVibrationLength = lengthInSeconds;
	}

	internal void SetKillStreakVibrationLength0(
		int lengthInSeconds
	)
	{
		Console.WriteLine(
			text: $"Set Kill Streak 0 Vibration Length to {lengthInSeconds} seconds."
		);
		this.m_killStreakVibrationLength0 = lengthInSeconds;
	}
	
	internal void SetKillStreakVibrationLength5(
		int lengthInSeconds
	)
	{
		Console.WriteLine(
			text: $"Set Kill Streak 5 Vibration Length to {lengthInSeconds} seconds."
		);
		this.m_killStreakVibrationLength5 = lengthInSeconds;
	}
	
	internal void SetKillStreakVibrationLength10(
		int lengthInSeconds
	)
	{
		Console.WriteLine(
			text: $"Set Kill Streak 10 Vibration Length to {lengthInSeconds} seconds."
		);
		this.m_killStreakVibrationLength10 = lengthInSeconds;
	}
	
	internal void SetKillStreakVibrationLength15(
		int lengthInSeconds
	)
	{
		Console.WriteLine(
			text: $"Set Kill Streak 15 Vibration Length to {lengthInSeconds} seconds."
		);
		this.m_killStreakVibrationLength15 = lengthInSeconds;
	}
	
	internal void SetKillStreakVibrationLength20(
		int lengthInSeconds
	)
	{
		Console.WriteLine(
			text: $"Set Kill Streak 20 Vibration Length to {lengthInSeconds} seconds."
		);
		this.m_killStreakVibrationLength20 = lengthInSeconds;
	}
	
	internal void SetKillStreakVibrationLength25(
		int lengthInSeconds
	)
	{
		Console.WriteLine(
			text: $"Set Kill Streak 25 Vibration Length to {lengthInSeconds} seconds."
		);
		this.m_killStreakVibrationLength25 = lengthInSeconds;
	}
	
	internal void SetKillVibrationIntensityCritical(
		int intensity
	)
	{
		Console.WriteLine(
			text: $"Set Kill Vibration Intensity Critical to {intensity}."
		);
		this.m_killVibrationIntensityCritical = intensity;
	}
	
	internal void SetKillVibrationIntensityNormal(
		int intensity
	)
	{
		Console.WriteLine(
			text: $"Set Kill Vibration Intensity Normal to {intensity}."
		);
		this.m_killVibrationIntensityNormal = intensity;
	}

	internal void SetSteamDisplayName(
		string displayName
	)
	{
		Console.WriteLine(
			text: $"Set Steam Display Name to {displayName}."
		);
		this.m_steamDisplayName = displayName;
	}
	
	internal void SetSuicideVibrationIntensity(
		int intensity
	)
	{
		Console.WriteLine(
			text: $"Set Suicide Vibration Intensity to {intensity}."
		);
		this.m_suicideVibrationIntensity = intensity;
	}

	internal void StartReadingConsoleLog()
	{
		this.m_cancellationTokenSource.Cancel();
		this.m_cancellationTokenSource = new CancellationTokenSource();
		var cancellationToken = this.m_cancellationTokenSource.Token;
		
		Task.Run(
			function:          async () =>
			{
				var path = this.m_tfPath.PathJoin(
					file: ServiceTeamFortress2.c_consoleLogFileName
				);
				try
				{
					await using var stream = new FileStream(
						path:   path, 
						mode:   FileMode.Open, 
						access: System.IO.FileAccess.Read, 
						share:  FileShare.ReadWrite
					);
					using var reader = new StreamReader(
						stream: stream
					);
	
					stream.Seek(
						offset: 0,
						origin: SeekOrigin.End
					);
					
					Console.WriteLine(
						text: $"Started Team Fortress 2 console.log parsing."
					);
					
					var serviceLovense = Services.GetService<ServiceLovense>();
	
					while (!cancellationToken.IsCancellationRequested) {
						var line = await reader.ReadLineAsync(
							cancellationToken: cancellationToken
						);
						if (line is null)
						{
							await Task.Delay(
								millisecondsDelay: ServiceTeamFortress2.c_consoleLogReadDelay, 
								cancellationToken: cancellationToken
							);
							continue;
						}
						
						var bhopCheckpointRegex = ServiceTeamFortress2.BhopRegexCheckpoint();
						if (
							bhopCheckpointRegex.IsMatch(
								input: line
							) is true
						)
						{
							this.LogBhopTimeToConsole(
								bhopTime: line
							);
							
							var isBetterTime = ServiceTeamFortress2.IsBetterTime(
								line: line
							);
							serviceLovense.Vibrate(
								intensity:     isBetterTime ? 20 : 10,
								timeInSeconds: isBetterTime ? 10 : 5
							);
							continue;
						}
						
						var bhopStageRegex = ServiceTeamFortress2.BhopRegexStage();
						if (
							bhopStageRegex.IsMatch(
								input: line
							) is true
						)
						{
							this.LogBhopTimeToConsole(
								bhopTime: line
							);
							
							var isBetterTime = ServiceTeamFortress2.IsBetterTime(
								line: line
							);
							serviceLovense.Vibrate(
								intensity:     isBetterTime ? 20 : 10,
								timeInSeconds: isBetterTime ? 30 : 15
							);
							continue;
						}
	
						if (
							line.Contains(
								value: this.m_steamDisplayName
							) is false
						)
						{
							continue;
						}
						
						if (
							line.Contains(
								value: "lovense"
							) is true
						)
						{
							Console.WriteLine(
								text: $"Triggering TF2 lovense echo."
							);
							serviceLovense.Vibrate(
								intensity:     this.m_echoVibrationIntensity,
								timeInSeconds: this.m_echoVibrationLength
							);
							continue;
						}
						
						if (
							line.Contains(
								value: "defended"
							) is true ||
							line.Contains(
								value: "captured"
							) is true
						)
						{
							serviceLovense.Vibrate(
								intensity:     20,
								timeInSeconds: 5
							);
							continue;
						}
						
						if (
							line.Contains(
								value: "suicided."
							) is true
						)
						{
							this.ResetKillStreak();
							
							serviceLovense.Vibrate(
								intensity:     this.m_suicideVibrationIntensity,
								timeInSeconds: this.GetKillStreakVibrationLengthInSeconds()
							);
							continue;
						}
						
						if (
							line.Contains(
								value: "Finished the Map"
							) is true
						)
						{
							this.LogBhopTimeToConsole(
								bhopTime: line
							);
							
							var isBetterTime = ServiceTeamFortress2.IsBetterTime(
								line: line
							);
							serviceLovense.Vibrate(
								intensity:     isBetterTime ? 20 : 10,
								timeInSeconds: isBetterTime ? 60 : 30
							);
							continue;
						}
						
						if (
							line.Contains(
								value: "Finished Bonus"
							) is true
						)
						{
							this.LogBhopTimeToConsole(
								bhopTime: line
							);
							
							var isBetterTime = ServiceTeamFortress2.IsBetterTime(
								line: line
							);
							serviceLovense.Vibrate(
								intensity:     isBetterTime ? 20 : 10,
								timeInSeconds: isBetterTime ? 30 : 15
							);
							continue;
						}
						
						var isCritical = line.Contains(
							value: "(crit)"
						);
						
						if (
							line.Contains(
								value: $"{this.m_steamDisplayName} killed"
							) is true
						)
						{
							this.IncreaseKillStreak();
							
							serviceLovense.Vibrate(
								intensity:     isCritical ? this.m_killVibrationIntensityCritical : this.m_killVibrationIntensityNormal, 
								timeInSeconds: this.GetKillStreakVibrationLengthInSeconds()
							);
						}
						else if (
							line.Contains(
								value: $"killed {this.m_steamDisplayName}"
							) is true
						)
						{
							this.ResetKillStreak();
							
							serviceLovense.Vibrate(
								intensity:     isCritical ? this.m_deathVibrationIntensityCritical : this.m_deathVibrationIntensityNormal, 
								timeInSeconds: this.GetKillStreakVibrationLengthInSeconds()
							);
						}
					}
				}
				catch (Exception exception)
				{
					if (exception is System.IO.DirectoryNotFoundException directoryNotFoundException)
					{
						Console.WriteLine(
							text: "Team Fortress /tf/ path is invalid. Right click Team Fortress 2 in Steam, select Manage, & select Browse local files. Set the path of the /tf/ folder in the config."
						);
					}
					else
					{
						Console.WriteLine(
							text: $"ServiceTeamFortress2 Exception: {exception.Message}"
						);
					}
				}
				
				Console.WriteLine(
					text: $"Stopped Team Fortress 2 console.log parsing."
				);
			},
			cancellationToken: cancellationToken
		);
	}

	internal void StopReadingConsoleLog()
	{
		this.m_cancellationTokenSource.Cancel();
	}
	
	[GeneratedRegex(
		pattern: @"Finished Bonus \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex BhopRegexBonus();
	
	[GeneratedRegex(
		pattern: @"Checkpoint \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex BhopRegexCheckpoint();
	
	[GeneratedRegex(
		pattern: @"Finished the Map\s+([\d:.]+)(?=\s+Rec\.)"
	)]
	private static partial Regex BhopRegexFinished();
	
	[GeneratedRegex(
		pattern: @"(?<=Per\.\s+)([+-][\d:.]+)\b"
	)]
	private static partial Regex BhopRegexPersonal();
	
	[GeneratedRegex(
		pattern: @"Finished Stage \d+(?=\s+([\d:.]+))"
	)]
	private static partial Regex BhopRegexStage();
	
	private const int               c_consoleLogReadDelay             = 20;
	private const string            c_consoleLogFileName              = "console.log";
	
	private CancellationTokenSource m_cancellationTokenSource         = new();
	private int                     m_killStreak                      = 0;
	private string				    m_steamDisplayName                = string.Empty;
	private string                  m_tfPath                          = string.Empty;
	    
	private int                     m_deathVibrationIntensityCritical = ServiceTeamFortress2Constants.DeathVibrationIntensityCritical;
	private int                     m_deathVibrationIntensityNormal   = ServiceTeamFortress2Constants.DeathVibrationIntensityNormal;
	
	private int                     m_echoVibrationIntensity          = ServiceTeamFortress2Constants.EchoVibrationIntensity;
	private int                     m_echoVibrationLength             = ServiceTeamFortress2Constants.EchoVibrationLength;
	
	private int                     m_killStreakVibrationLength0      = ServiceTeamFortress2Constants.KillStreakVibrationLength0;
	private int                     m_killStreakVibrationLength5      = ServiceTeamFortress2Constants.KillStreakVibrationLength5;
	private int                     m_killStreakVibrationLength10     = ServiceTeamFortress2Constants.KillStreakVibrationLength10;
	private int                     m_killStreakVibrationLength15     = ServiceTeamFortress2Constants.KillStreakVibrationLength15;
	private int                     m_killStreakVibrationLength20     = ServiceTeamFortress2Constants.KillStreakVibrationLength20;
	private int                     m_killStreakVibrationLength25     = ServiceTeamFortress2Constants.KillStreakVibrationLength25;
	
	private int                     m_killVibrationIntensityCritical  = ServiceTeamFortress2Constants.KillVibrationIntensityCritical;
	private int                     m_killVibrationIntensityNormal    = ServiceTeamFortress2Constants.KillVibrationIntensityNormal;
	
	private int                     m_suicideVibrationIntensity       = ServiceTeamFortress2Constants.SuicideVibrationIntensity;

	private int GetKillStreakVibrationLengthInSeconds()
	{
		return this.m_killStreak switch
		{
			>= 5  and < 10 => this.m_killStreakVibrationLength5,
			>= 10 and < 15 => this.m_killStreakVibrationLength10,
			>= 15 and < 20 => this.m_killStreakVibrationLength15,
			>= 20 and < 25 => this.m_killStreakVibrationLength20,
			>= 25          => this.m_killStreakVibrationLength25,
			_              => this.m_killStreakVibrationLength0
		};
	}

	private void IncreaseKillStreak()
	{
		this.m_killStreak++;
		Console.WriteLine(
			text: $"KILLSTREAK: {this.m_killStreak}"
		);
	}
	
	private static bool IsBetterTime(
		string line
	)
	{
		return line.Contains(
			value: '-'
	   	) is true || 
	   	line.Contains(
		  	value: "Per."
	   	) is false;
	}

	private void LogBhopTimeToConsole(
		string bhopTime
	)
	{
		var bhopPersonalRegex = ServiceTeamFortress2.BhopRegexPersonal();
		var bhopPersonalMatch = bhopPersonalRegex.Match(
			input: bhopTime
		);
		var bhopPersonalTimeExists = bhopPersonalMatch.Success;
		var bhopPersonalTime = $"{(bhopPersonalTimeExists ? $" PR: {bhopPersonalMatch.Value}" : string.Empty)}";
		
		var bhopCheckpointRegex = ServiceTeamFortress2.BhopRegexCheckpoint();
		var bhopCheckpointMatch = bhopCheckpointRegex.Match(
			input: bhopTime
		);
		
		var isCheckpoint = bhopCheckpointMatch.Success;
		if (isCheckpoint is true)
		{
			Console.WriteLine(
				text: $"{this.m_steamDisplayName} Reached {bhopCheckpointMatch.Groups[0].Value} in {bhopCheckpointMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopStageRegex = ServiceTeamFortress2.BhopRegexStage();
		var bhopStageMatch = bhopStageRegex.Match(
			input: bhopTime
		);
		
		var isStage = bhopStageMatch.Success;
		if (isStage is true)
		{
			Console.WriteLine(
				text: $"{this.m_steamDisplayName} {bhopStageMatch.Groups[0].Value} in {bhopStageMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopFinishedRegex = ServiceTeamFortress2.BhopRegexFinished();
		var bhopFinishedMatch = bhopFinishedRegex.Match(
			input: bhopTime
		);
		
		var isFinished = bhopFinishedMatch.Success;
		if (isFinished is true)
		{
			Console.WriteLine(
				text: $"{this.m_steamDisplayName} Finished the Map in {bhopFinishedMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
			return;
		}
		
		var bhopBonusRegex = ServiceTeamFortress2.BhopRegexBonus();
		var bhopBonusMatch = bhopBonusRegex.Match(
			input: bhopTime
		);
		
		var isBonus = bhopBonusMatch.Success;
		if (isBonus is true)
		{
			Console.WriteLine(
				text: $"{this.m_steamDisplayName} {bhopBonusMatch.Groups[0].Value} in {bhopBonusMatch.Groups[1].Value}!{bhopPersonalTime}"
			);
		}
	}
	
	private void ResetKillStreak()
	{
		this.m_killStreak = 0;
		Console.WriteLine(
			text: $"Kill streak reset."
		);
	}
}
