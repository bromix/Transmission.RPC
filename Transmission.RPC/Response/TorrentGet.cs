using System.Text.Json.Serialization;

namespace Transmission.RPC.Response;

public class TorrentGet: Base
{
    [JsonPropertyName("arguments")]
    public Dictionary<string, object>? Arguments { get; set; }
}