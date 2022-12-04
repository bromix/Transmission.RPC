namespace Transmission.RPC.Responses;

public sealed record TorrentAddInfo
{
    public string HashString { get; init; } = default!;
    public int Id { get; init; } = default!;
    public string Name { get; init; } = default!;
}