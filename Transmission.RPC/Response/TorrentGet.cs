using System.Text.Json.Serialization;

namespace Transmission.RPC.Response;

public class TorrentGetArguments
{
    public Torrent[]? Torrents { get; init; }
}

public class TorrentGet : Base
{
    [JsonPropertyName("arguments")]
    public TorrentGetArguments? Arguments { get; set; }
}