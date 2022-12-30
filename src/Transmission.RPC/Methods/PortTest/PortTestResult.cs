using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.PortTest;

public sealed record PortTestResult
{
    [JsonPropertyName("port-is-open")] public bool PortIsOpen { get; init; } = false;
}