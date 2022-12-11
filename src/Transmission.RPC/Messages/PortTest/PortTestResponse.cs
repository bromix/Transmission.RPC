using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.PortTest;

public sealed record PortTestResponse
{
    [JsonPropertyName("port-is-open")] public bool PortIsOpen { get; init; }
}