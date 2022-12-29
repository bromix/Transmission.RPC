using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentVerify;

/// <summary>
/// Method: "torrent-start"
/// </summary>
[TransmissionMethod("torrent-verify")]
public sealed record TorrentVerifyRequest : ITransmissionRequest
{
    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}