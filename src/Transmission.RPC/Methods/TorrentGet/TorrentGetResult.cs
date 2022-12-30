namespace Transmission.RPC.Methods.TorrentGet;

public sealed record TorrentGetResult
{
    public Torrent[]? Torrents { get; init; } = null;
}