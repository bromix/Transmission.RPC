using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentReannounce;

/// <summary>
/// Method: "torrent-start"
/// </summary>
[TransmissionMethod("torrent-reannounce")]
public sealed record TorrentReannounceRequest: ITransmissionRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}