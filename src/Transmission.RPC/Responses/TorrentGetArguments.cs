namespace Transmission.RPC.Responses;

public sealed record TorrentGetArguments
{
    public Torrent[]? Torrents { get; init; } = null;
}