namespace Transmission.RPC.Requests;

/// <summary>
/// Record to allow usage of int and string torrent ids at the same time.
/// </summary>
public sealed record RequestTorrentId
{
    private RequestTorrentId(int id) => Id = id;
    private RequestTorrentId(string hashString) => Id = hashString;
    public object Id { get; }

    public static implicit operator RequestTorrentId(int id) => new(id);
    public static implicit operator RequestTorrentId(string hashString) => new(hashString);
}