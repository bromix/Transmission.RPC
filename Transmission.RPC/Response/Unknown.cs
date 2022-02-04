using System.Text.Json.Serialization;

namespace Transmission.RPC.Response;

public class Unknown: Base
{
    [JsonPropertyName("arguments")]
    public Dictionary<string, object>? Arguments { get; set; }
}