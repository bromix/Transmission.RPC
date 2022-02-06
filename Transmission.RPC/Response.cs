using System.Text.Json.Serialization;

namespace Transmission.RPC;

public class Base<TArguments>
{
    public string Result { get; init; } = default!;
    public TArguments? Arguments { get; init; }
    public int? Tag { get; init; }
}


public class TorrentGetResponseArguments
{
    public Torrent[]? Torrents { get; init; }
}
public class TorrentGetResponse : Base<TorrentGetResponseArguments> { }


public class TorrentAddInfo
{
    public string HashString { get; init; } = default!;
    public int id { get; init; } = default!;
    public string Name { get; init; } = default!;
}
public class TorrentAddResponseArguments
{
    [JsonPropertyName("torrent-added")]
    public TorrentAddInfo? TorrentAdded { get; init; }

    [JsonPropertyName("torrent-duplicate")]
    public TorrentAddInfo? TorrentDuplicate { get; init; }
}

public class TorrentAddResponse : Base<TorrentAddResponseArguments> { }