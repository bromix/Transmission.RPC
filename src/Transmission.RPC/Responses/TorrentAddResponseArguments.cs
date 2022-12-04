using System.Text.Json.Serialization;

namespace Transmission.RPC.Responses;

public sealed record TorrentAddResponseArguments
{
    [JsonPropertyName("torrent-added")] public TorrentAddInfo? TorrentAdded { get; init; }

    [JsonPropertyName("torrent-duplicate")]
    public TorrentAddInfo? TorrentDuplicate { get; init; }
}