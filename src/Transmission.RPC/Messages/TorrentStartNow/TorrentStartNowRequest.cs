using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentStartNow;

/// <summary>
/// Method: "torrent-start"
/// </summary>
[TransmissionMethod("torrent-start-now")]
public sealed record TorrentStartNowRequest: ITransmissionRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}