using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.TorrentAdd;

public sealed record TorrentAddResult
{
    [JsonPropertyName("torrent-added")] public TorrentAddInfo? TorrentAdded { get; init; } = null;

    [JsonPropertyName("torrent-duplicate")]
    public TorrentAddInfo? TorrentDuplicate { get; init; } = null;
}