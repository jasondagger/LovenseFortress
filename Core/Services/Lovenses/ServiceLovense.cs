
using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;
using LovenseFortress.Core.Content;
using LovenseFortress.Core.Services.Godots;
using ServiceGodotHttp = LovenseFortress.Core.Services.Godots.Https.ServiceGodotHttp;

namespace LovenseFortress.Core.Services.Lovenses;

internal sealed class ServiceLovense() :
    IService
{
    Task IService.Setup()
    {
        this.RetrieveResources();
        return Task.CompletedTask;
    }
    
    Task IService.Start()
    {
        this.StartProcessCommands();
        return Task.CompletedTask;
    }
    
    Task IService.Stop()
    {
        this.StopProcessingCommands();
        return Task.CompletedTask;
    }
    
    internal void All(
        int    intensity, 
        double timeInSeconds
    )
    {
        Console.WriteLine(
            text: $"All effects activated toy by {intensity} for {timeInSeconds} seconds."
        );
        
        var command = new ServiceLovenseCommand(
            action:        $"All:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity
        );
    }
    
    internal void Oscillate(
        int    intensity, 
        double timeInSeconds
    )
    {
        Console.WriteLine(
            text: $"Oscillating toy by {intensity} for {timeInSeconds} seconds."
        );
        
        var command = new ServiceLovenseCommand(
            action:        $"Oscillate:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity
        );
    }

    internal void SetDomain(
        string domain
    )
    {
        Console.WriteLine(
            text: $"Set Lovense Domain to {domain}."
        );
        
        this.m_domain = domain;
    }

    internal void SetPort(
        string port
    )
    {
        Console.WriteLine(
            text: $"Set Lovense port to {port}."
        );
        
        this.m_port = port;
    }

    internal void Vibrate(
        int    intensity, 
        double timeInSeconds
    )
    {
        Console.WriteLine(
            text: $"Vibrating toy by {intensity} for {timeInSeconds} seconds."
        );
        
        var command = new ServiceLovenseCommand(
            action:        $"Vibrate:{intensity}",
            timeInSeconds: timeInSeconds
        );

        this.AddCommandToQueue(
            command:       command,
            timeInSeconds: timeInSeconds,
            intensity:     intensity
        );
    }
    
    private sealed class ServiceLovenseCommandData(
        ServiceLovenseCommand command,
        int                   timeInMilliseconds,
        int                   intensity
    )
    {
        public readonly ServiceLovenseCommand Command            = command;
        public readonly int                   TimeInMilliseconds = timeInMilliseconds;
        public readonly int                   Intensity          = intensity;
    }
    
    private const int                                 c_processDelayInMilliseconds = 20;
    private string                                    m_domain                     = "192.168.88.3";
    private string                                    m_port                       = "20011";
    private ServiceGodotHttp                          m_serviceGodotHttp           = null;
    private readonly Queue<ServiceLovenseCommandData> m_queueCommandDatas          = new();
    private readonly object                           m_lock                       = new();
    private bool                                      m_shutdown                   = false;

    private void AddCommandToQueue(
        ServiceLovenseCommand command,
        double                timeInSeconds,
        int                   intensity
    )
    {
        var timeInMilliseconds = (int)(timeInSeconds * 1000);
        
        var commandData = new ServiceLovenseCommandData(
            command:            command,
            timeInMilliseconds: timeInMilliseconds,
            intensity:          intensity
        );
        
        lock (this.m_lock)
        {
            this.m_queueCommandDatas.Enqueue(
                item: commandData
            );
        }
    }
    
    private void RetrieveResources()
    {
        var serviceGodots       = Services.GetService<ServiceGodots>();
        this.m_serviceGodotHttp = serviceGodots.GetServiceGodot<ServiceGodotHttp>();
    }
    
    private void StartProcessCommands()
    {
        Task.Run(
            function: async () =>
            {
                Console.WriteLine(
                    text: $"Starting Lovense Service."
                );
                
                while (this.m_shutdown is false)
                {
                    await Task.Delay(
                        millisecondsDelay: ServiceLovense.c_processDelayInMilliseconds
                    );
                    
                    ServiceLovenseCommandData commandData;
                    lock (this.m_lock)
                    {
                        if (this.m_queueCommandDatas.Count > 0U)
                        {
                            commandData = this.m_queueCommandDatas.Dequeue();
                        }
                        else
                        {
                            continue;
                        }
                    }

                    var json = System.Text.Json.JsonSerializer.Serialize(
                        value: commandData.Command
                    );
                    this.m_serviceGodotHttp.SendHttpRequest(
                        url:                     $"http://{this.m_domain}:{this.m_port}/command",
                        headers:                 [
                            $"Content-Type: application/json",
                        ],
                        method:                  HttpClient.Method.Post,
                        json:                    json,
                        requestCompletedHandler: (
                            long     result,
                            long     responseCode,
                            string[] headers,
                            byte[]   body
                        ) =>
                        {
                            Console.WriteLine(
                                text: $"Lovense Service: Response code {responseCode}."
                            );
                            if (responseCode >= 300)
                            {
                                
                            }
                        }
                    );

                    await Task.Delay(
                        millisecondsDelay: commandData.TimeInMilliseconds
                    );
                }
            }
        );
    }
    
    private void StopProcessingCommands()
    {
        this.m_shutdown = true;
    }
}