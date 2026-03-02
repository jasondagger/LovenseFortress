using System;
using System.Text.Json.Serialization;

namespace LovenseFortress.Core.Services.Lovenses.Payloads;

[Serializable()]
public sealed class ServiceLovensePayloadAuthorizationToken()
{
    [JsonPropertyName(
        name: $"authToken"
    )]
    public string AuthorizationToken { get; set; } = _ = string.Empty;
}