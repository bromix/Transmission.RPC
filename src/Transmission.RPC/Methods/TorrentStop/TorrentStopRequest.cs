using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.TorrentStop;

/// <summary>
/// Method: "torrent-start"
/// </summary>
[TransmissionMethod("torrent-stop")]
public sealed record TorrentStopRequest: ITransmissionRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}