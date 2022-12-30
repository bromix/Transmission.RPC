namespace Transmission.RPC.Methods.TorrentGet;

public sealed record FileStat
{
    public long BytesCompleted { get; init; }
    public bool Wanted { get; init; }
    public int Priority { get; init; }
}