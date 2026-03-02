
using System;
using System.Text.Json.Serialization;

namespace LovenseFortress.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorizationValidation()
{
    [JsonPropertyName(
        name: $"platform"
    )]
    public string       Platform           { get; set; } = string.Empty;
    
    [JsonPropertyName(
        name: $"authToken"
    )]
    public string       AuthorizationToken { get; set; } = string.Empty;
}