using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentReannounce;

/// <summary>
/// Method: "torrent-start"
/// </summary>
public sealed record TorrentReannounceRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}