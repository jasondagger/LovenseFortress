
using System;
using System.Text.Json.Serialization;

namespace LovenseFortress.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorizationValidationResultData()
{
    [JsonPropertyName(
        name: $"socketIoPath"
    )]
    public string       SocketIoPath     { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"socketIoUrl"
    )]
    public string       SocketIoUrl    { get; set; } = string.Empty;
}