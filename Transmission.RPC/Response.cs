using System.Text.Json.Serialization;

namespace Transmission.RPC;

public class Response
{
    [JsonPropertyName("result")]
    public string Result { get; init; } = default!;

    [JsonPropertyName("arguments")]
    public Dictionary<string, object>? Arguments { get; set; }
}
