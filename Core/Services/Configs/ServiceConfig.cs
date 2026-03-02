
using Godot;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LovenseFortress.Core.Content;
using LovenseFortress.Core.Services.Lovenses;
using LovenseFortress.Core.Services.TeamFortress2s;
using FileAccess = Godot.FileAccess;

namespace LovenseFortress.Core.Services.Configs;

public sealed partial class ServiceConfig() : 
	IService
{
	async Task IService.Setup()
	{
		await this.LoadConfigFile();
	}
    
	Task IService.Start()
	{
		return Task.CompletedTask;
	}
    
	Task IService.Stop()
	{
		return Task.CompletedTask;
	}
	
	internal async Task LoadConfigFile()
	{
		var path = ServiceConfig.GetPath();
		
		if (
			File.Exists(
				path: path
			) is false
		)
		{
			Console.WriteLine(
				text: $"Config file does not exist. Generating a new one @ {path}."
			);
			await ServiceConfig.CreateConfigFile(
				path: path
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Processing config file..."
			);
			await this.ProcessConfigFile(
				path: path
			);
		}
	}

	internal string GetLovenseIP()
	{
		return this.m_lovenseIP;
	}

	internal string GetLovensePort()
	{
		return this.m_lovensePort;
	}

	internal string GetSteamDisplayName()
	{
		return this.m_steamDisplayName;
	}

	internal string GetTeamFortress2Path()
	{
		return this.m_teamFortress2Path;
	}

	internal void UpdateConfigFile(
		string configConstant,
		string value
	)
	{
		var path  = ServiceConfig.GetPath();
		var lines = FileAccess.GetFileAsString(
			path: path
		).Split(
			separator: '\n'
		);

		for (var i = 0; i < lines.Length; i++)
		{
			if (
				lines[i].StartsWith(
					value: configConstant
				) is false
			)
			{
				continue;
			}
			
			lines[i] = $"{configConstant}={value}";
			break;
		}

		using var file = FileAccess.Open(
			path:  path, 
			flags: FileAccess.ModeFlags.Write
		);
		file.StoreString(
			string.Join(
				separator: "\n",
				value:     lines
			)
		);
		
		Task.Run(
			function: async () =>
			{
				await this.LoadConfigFile();
			}
		);
	}
	
	private const string c_delimiter         = "=";
	private const string c_fileName          = "config.txt";
	private const string c_userPath          = "user://";
	
	private string       m_lovenseIP         = string.Empty;
	private string       m_lovensePort       = string.Empty;
	private string       m_steamDisplayName  = string.Empty;
	private string       m_teamFortress2Path = string.Empty;
	
	private static async Task CreateConfigFile(
		string path
	)
	{
		var osName = OS.GetName();
		var tfPath = osName switch
		{
			"Windows" => @"C:\Program Files (x86)\Steam\steamapps\common\Team Fortress 2\tf\",
			"Linux"   => Path.Combine(
							path1: OS.GetEnvironment(
								variable: "HOME"
							),
							path2: ".steam/steam/steamapps/common/Team Fortress 2/tf/"
						),
			_         => string.Empty
		};

		await File.WriteAllTextAsync(
			path:     path,
			contents: $"{ ServiceConfigConstants.SteamDisplayName                }{ ServiceConfig.c_delimiter }\n" +
			          $"{ ServiceConfigConstants.TeamFortress2Path                          }{ ServiceConfig.c_delimiter }{tfPath}\n" +
			          $"{ ServiceConfigConstants.LovenseLocalIP                   }{ ServiceConfig.c_delimiter }\n" +
			          $"{ ServiceConfigConstants.LovensePort                     }{ ServiceConfig.c_delimiter }\n\n" +
			          $"{ ServiceConfigConstants.DeathVibrationIntensityCritical }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.DeathVibrationIntensityCritical }\n" +
			          $"{ ServiceConfigConstants.DeathVibrationIntensityNormal   }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.DeathVibrationIntensityNormal   }\n" +
			          $"{ ServiceConfigConstants.EchoVibrationIntensity          }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.EchoVibrationIntensity          }\n" +
			          $"{ ServiceConfigConstants.EchoVibrationLength             }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.EchoVibrationLength             }\n" +
			          $"{ ServiceConfigConstants.KillStreakVibrationLength0      }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillStreakVibrationLength0      }\n" +
			          $"{ ServiceConfigConstants.KillStreakVibrationLength5      }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillStreakVibrationLength5      }\n" +
			          $"{ ServiceConfigConstants.KillStreakVibrationLength10     }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillStreakVibrationLength10     }\n" +
			          $"{ ServiceConfigConstants.KillStreakVibrationLength15     }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillStreakVibrationLength15     }\n" +
			          $"{ ServiceConfigConstants.KillStreakVibrationLength20     }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillStreakVibrationLength20     }\n" +
			          $"{ ServiceConfigConstants.KillStreakVibrationLength25     }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillStreakVibrationLength25     }\n" +
			          $"{ ServiceConfigConstants.KillVibrationIntensityCritical  }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillVibrationIntensityCritical  }\n" +
			          $"{ ServiceConfigConstants.KillVibrationIntensityNormal    }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.KillVibrationIntensityNormal    }\n" +
			          $"{ ServiceConfigConstants.SuicideVibrationIntensity       }{ ServiceConfig.c_delimiter }{ ServiceTeamFortress2Constants.SuicideVibrationIntensity       }\n"
		);
	}

	private static string GetPath()
	{
		return Path.Combine(
			paths:
			[
				ProjectSettings.GlobalizePath(
					path: ServiceConfig.c_userPath
				),
				ServiceConfig.c_fileName
			]
		);
	}
	
	private async Task ProcessConfigFile(
		string path
	)
	{
		var config = await File.ReadAllLinesAsync(
			path: path
		);
		
		var configDictionary = config.Select(
			selector: line => line.Split(
				separator: ServiceConfig.c_delimiter,
				count:     2
			)
		).ToDictionary(
			keySelector: parts => parts[0], 
			             parts => parts.Length > 1 ? parts[1] : string.Empty
		);
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.SteamDisplayName
			) is false ||
			configDictionary[key: ServiceConfigConstants.SteamDisplayName] == string.Empty
		)
		{
			Console.WriteLine(
				text: $"Assign your Steam Display Name to {ServiceConfigConstants.SteamDisplayName} in the config file."
			);
			return;
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.TeamFortress2Path
			) is false ||
			configDictionary[key: ServiceConfigConstants.TeamFortress2Path] == string.Empty
		)
		{
			Console.WriteLine(
				text: $"Assign your Team Fortress 2 /tf/ folder path to {ServiceConfigConstants.TeamFortress2Path} in the config file."
			);
			return;
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.LovenseLocalIP
			) is false ||
			configDictionary[key: ServiceConfigConstants.LovenseLocalIP] == string.Empty
		)
		{
			Console.WriteLine(
				text: $"Assign your Lovense Remote domain to {ServiceConfigConstants.LovenseLocalIP} in the config file."
			);
			return;
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.LovensePort
			) is false || 
			configDictionary[key: ServiceConfigConstants.LovensePort] == string.Empty
		)
		{
			Console.WriteLine(
				text: $"Assign your Lovense Remote port to {ServiceConfigConstants.LovensePort} in the config file."
			);
			return;
		}

		var steamDisplayName = configDictionary[key: ServiceConfigConstants.SteamDisplayName];
		var tfPath           = configDictionary[key: ServiceConfigConstants.TeamFortress2Path];
		
		var serviceTeamFortress2 = Services.GetService<ServiceTeamFortress2>();
		serviceTeamFortress2.SetTFPath(
			tfPath: tfPath
		);
		serviceTeamFortress2.SetSteamDisplayName(
			displayName: steamDisplayName
		);
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.DeathVibrationIntensityCritical
			) is true &&
			configDictionary[key: ServiceConfigConstants.DeathVibrationIntensityCritical] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.DeathVibrationIntensityCritical], 
				result: out var value
			) is true
		)
		{
			serviceTeamFortress2.SetDeathVibrationIntensityCritical(
				intensity: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.DeathVibrationIntensityCritical}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.DeathVibrationIntensityNormal
			) is true &&
			configDictionary[key: ServiceConfigConstants.DeathVibrationIntensityNormal] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.DeathVibrationIntensityNormal], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetDeathVibrationIntensityNormal(
				intensity: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.DeathVibrationIntensityNormal}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.EchoVibrationIntensity
			) is true &&
			configDictionary[key: ServiceConfigConstants.EchoVibrationIntensity] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.EchoVibrationIntensity], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetEchoVibrationIntensity(
				intensity: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.EchoVibrationIntensity}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.EchoVibrationLength
			) is true &&
			configDictionary[key: ServiceConfigConstants.EchoVibrationLength] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.EchoVibrationLength], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetEchoVibrationLength(
				lengthInSeconds: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.EchoVibrationLength}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillStreakVibrationLength0
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength0] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength0], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillStreakVibrationLength0(
				lengthInSeconds: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillStreakVibrationLength0}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillStreakVibrationLength5
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength5] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength5], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillStreakVibrationLength5(
				lengthInSeconds: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillStreakVibrationLength5}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillStreakVibrationLength10
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength10] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength10], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillStreakVibrationLength10(
				lengthInSeconds: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillStreakVibrationLength10}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillStreakVibrationLength15
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength15] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength15], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillStreakVibrationLength15(
				lengthInSeconds: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillStreakVibrationLength15}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillStreakVibrationLength20
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength20] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength20], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillStreakVibrationLength20(
				lengthInSeconds: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillStreakVibrationLength20}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillStreakVibrationLength25
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength25] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillStreakVibrationLength25], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillStreakVibrationLength25(
				lengthInSeconds: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillStreakVibrationLength25}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillVibrationIntensityCritical
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillVibrationIntensityCritical] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillVibrationIntensityCritical], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillVibrationIntensityCritical(
				intensity: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillVibrationIntensityCritical}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.KillVibrationIntensityNormal
			) is true &&
			configDictionary[key: ServiceConfigConstants.KillVibrationIntensityNormal] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.KillVibrationIntensityNormal], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetKillVibrationIntensityNormal(
				intensity: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.KillVibrationIntensityNormal}."
			);
		}
		
		if (
			configDictionary.ContainsKey(
				key: ServiceConfigConstants.SuicideVibrationIntensity
			) is true &&
			configDictionary[key: ServiceConfigConstants.SuicideVibrationIntensity] != string.Empty &&
			int.TryParse(
				s:      configDictionary[key: ServiceConfigConstants.SuicideVibrationIntensity], 
				result: out value
			) is true
		)
		{
			serviceTeamFortress2.SetSuicideVibrationIntensity(
				intensity: value
			);
		}
		else
		{
			Console.WriteLine(
				text: $"Failed to parse value in {ServiceConfig.c_fileName} for {ServiceConfigConstants.SuicideVibrationIntensity}."
			);
		}
		
		var lovenseDomain = configDictionary[key: ServiceConfigConstants.LovenseLocalIP];
		var lovensePort   = configDictionary[key: ServiceConfigConstants.LovensePort];
		
		var serviceLovense = Services.GetService<ServiceLovense>();
		serviceLovense.SetDomain(
			domain: lovenseDomain
		);
		serviceLovense.SetPort(
			port: lovensePort
		);

		this.m_lovenseIP         = lovenseDomain;
		this.m_lovensePort       = lovensePort;
		this.m_steamDisplayName  = steamDisplayName;
		this.m_teamFortress2Path = tfPath;
	}
}
