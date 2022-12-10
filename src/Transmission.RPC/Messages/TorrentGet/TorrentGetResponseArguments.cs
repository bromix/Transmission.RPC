namespace Transmission.RPC.Messages.TorrentGet;

public sealed record TorrentGetResponseArguments
{
    public Torrent[]? Torrents { get; init; } = null;
}