using System.Text.Json.Serialization;

namespace Transmission.RPC.Responses;

public sealed record TorrentAddArguments
{
    [JsonPropertyName("torrent-added")] public TorrentAddInfo? TorrentAdded { get; init; } = null;

    [JsonPropertyName("torrent-duplicate")]
    public TorrentAddInfo? TorrentDuplicate { get; init; } = null;
}