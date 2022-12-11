using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentGet;

/// <summary>
/// Method: "torrent-get"
/// </summary>
public sealed record TorrentGetRequest
{
    [JsonPropertyName("fields")]
    [JsonConverter(typeof(TorrentGetRequestFieldTypeJsonConverter))]
    public TorrentGetRequestField[] Fields { get; set; } = Array.Empty<TorrentGetRequestField>();

    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}