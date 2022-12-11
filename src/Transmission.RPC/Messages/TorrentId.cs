namespace Transmission.RPC.Messages;

/// <summary>
/// Record to allow usage of int and string torrent ids at the same time.
/// </summary>
public sealed class TorrentId : IEquatable<TorrentId>
{
    private TorrentId(int id) => Id = id;
    private TorrentId(string hashString) => Id = hashString;
    public object Id { get; }

    public static implicit operator TorrentId(int id) => new(id);
    public static implicit operator TorrentId(string hashString) => new(hashString);

    public static bool operator ==(TorrentId left, TorrentId right) => Equals(left, right);
    public static bool operator !=(TorrentId left, TorrentId right) => !(left == right);

    public bool Equals(TorrentId? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        return Id.Equals(other.Id);
    }

    public override bool Equals(object? obj) => Equals(obj as TorrentId);
    public override int GetHashCode() => Id.GetHashCode();
}