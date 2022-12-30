namespace Transmission.RPC.Methods.TorrentGet;

public sealed record File
{
    public long BytesCompleted { get; init; }
    public long Length { get; init; }
    public string Name { get; init; } = string.Empty;
}