using System.Text.Json.Serialization;

namespace Transmission.RPC;

public record Base<TArguments>
{
    public string Result { get; init; } = default!;
    public TArguments? Arguments { get; init; }
    public int? Tag { get; init; }
}

public record TorrentGetResponseArguments
{
    public Torrent[]? Torrents { get; init; }
}

public sealed record TorrentGetResponse : Base<TorrentGetResponseArguments>
{
}

public sealed record TorrentAddInfo
{
    public string HashString { get; init; } = default!;
    public int Id { get; init; } = default!;
    public string Name { get; init; } = default!;
}

public sealed record TorrentAddResponseArguments
{
    [JsonPropertyName("torrent-added")] public TorrentAddInfo? TorrentAdded { get; init; }

    [JsonPropertyName("torrent-duplicate")]
    public TorrentAddInfo? TorrentDuplicate { get; init; }
}

public sealed record TorrentAddResponse : Base<TorrentAddResponseArguments>
{
}