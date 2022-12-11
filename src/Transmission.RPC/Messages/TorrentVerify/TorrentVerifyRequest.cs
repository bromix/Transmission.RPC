using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentVerify;

/// <summary>
/// Method: "torrent-start"
/// </summary>
public sealed record TorrentVerifyRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}