namespace Transmission.RPC.Messages.TorrentGet;

public sealed record TorrentGetResponse
{
    public Torrent[]? Torrents { get; init; } = null;
}