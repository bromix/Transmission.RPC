namespace Transmission.RPC.Responses;

public sealed record TorrentGetResponseArguments
{
    public Torrent[]? Torrents { get; init; } = null;
}