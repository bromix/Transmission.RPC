using System.Text.Json.Serialization;

namespace Transmission.RPC.Response;

public class TorrentAdded
{
    public string HashString { get; init; } = default!;
    public int id { get; init; } = default!;
    public string Name { get; init; } = default!;
}
public class TorrentAddArguments
{
    [JsonPropertyName("torrent-added")]
    public TorrentAdded? TorrentAdded { get; init; }

    [JsonPropertyName("torrent-duplicate")]
    public TorrentAdded? TorrentDuplicate { get; init; }
}

public class TorrentAdd : Base<TorrentAddArguments> { }