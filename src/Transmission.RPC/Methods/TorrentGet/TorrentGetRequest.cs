using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.TorrentGet;

/// <summary>
/// Method: "torrent-get"
/// </summary>
[TransmissionMethod("torrent-get")]
public sealed record TorrentGetRequest : ITransmissionRequest
{
    public TorrentGetRequest(TorrentGetRequestField[] fields) => Fields = fields;

    [JsonPropertyName("fields")]
    [JsonConverter(typeof(TorrentGetRequestFieldTypeJsonConverter))]
    public TorrentGetRequestField[] Fields { get; }

    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}