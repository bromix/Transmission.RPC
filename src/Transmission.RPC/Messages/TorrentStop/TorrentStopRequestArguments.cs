using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentStop;

/// <summary>
/// Method: "torrent-start"
/// </summary>
public sealed record TorrentStopRequestArguments
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}