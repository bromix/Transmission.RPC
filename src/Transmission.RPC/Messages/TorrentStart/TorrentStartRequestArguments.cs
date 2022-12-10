using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentStart;

/// <summary>
/// Method: "torrent-start"
/// </summary>
public sealed class TorrentStartRequestArguments
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}