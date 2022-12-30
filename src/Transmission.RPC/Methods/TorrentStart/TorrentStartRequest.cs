using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.TorrentStart;

/// <summary>
/// Method: "torrent-start"
/// </summary>
[TransmissionMethod("torrent-start")]
public sealed record TorrentStartRequest: ITransmissionRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}