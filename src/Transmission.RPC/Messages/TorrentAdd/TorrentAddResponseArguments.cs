using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.TorrentAdd;

public sealed record TorrentAddResponseArguments
{
    [JsonPropertyName("torrent-added")] public TorrentAddInfo? TorrentAdded { get; init; } = null;

    [JsonPropertyName("torrent-duplicate")]
    public TorrentAddInfo? TorrentDuplicate { get; init; } = null;
}