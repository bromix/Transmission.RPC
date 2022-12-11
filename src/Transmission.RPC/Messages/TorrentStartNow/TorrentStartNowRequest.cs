using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentStartNow;

/// <summary>
/// Method: "torrent-start"
/// </summary>
public sealed record TorrentStartNowRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}