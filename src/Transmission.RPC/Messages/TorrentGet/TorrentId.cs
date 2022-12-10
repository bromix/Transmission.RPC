namespace Transmission.RPC.Messages.TorrentGet;

/// <summary>
/// Record to allow usage of int and string torrent ids at the same time.
/// </summary>
public sealed record TorrentId
{
    private TorrentId(int id) => Id = id;
    private TorrentId(string hashString) => Id = hashString;
    public object Id { get; }

    public static implicit operator TorrentId(int id) => new(id);
    public static implicit operator TorrentId(string hashString) => new(hashString);
}