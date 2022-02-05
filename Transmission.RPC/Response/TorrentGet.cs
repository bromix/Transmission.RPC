namespace Transmission.RPC.Response;

public class TorrentGetArguments
{
    public Torrent[]? Torrents { get; init; }
}

public class TorrentGet : Base<TorrentGetArguments> { }