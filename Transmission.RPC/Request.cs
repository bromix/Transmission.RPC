using System.Text.Json.Serialization;

namespace Transmission.RPC;
public class Request
{
    private static int tagCounter = 0;
    public Request(string method)
    {
        Method = method;
        Tag = ++tagCounter;
    }

    [JsonPropertyName("method")]
    public string Method { get; init; }

    [JsonPropertyName("tag")]
    public int Tag { get; init; }

    [JsonPropertyName("arguments")]
    public object? arguments { get; set; }
}

/// <summary>
/// Method: "torrent-add"
/// </summary>
public class TorrentAddRequestArguments
{
    /// <summary>
    /// Filename or URL of the .torrent file.
    /// </summary>
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    /// <summary>
    /// Base64-encoded .torrent content.
    /// </summary>
    [JsonPropertyName("metainfo")]
    public string? metainfo { get; set; }

    /// <summary>
    /// Path to download the torrent to.
    /// </summary>
    [JsonPropertyName("download-dir")]
    public string? DownloadDir { get; set; }

    /// <summary>
    /// If true, don't start the torrent.
    /// </summary>
    [JsonPropertyName("paused")]
    public bool? Paused { get; set; }
}

/// <summary>
/// Method: "torrent-get"
/// </summary>
public class TorrentGetRequestArguments
{
    public enum FieldType
    {
        Id,
        Name
    };

    [JsonPropertyName("fields")]
    [JsonConverter(typeof(FieldTypeJsonConverter))]
    public List<FieldType> Fields { get; set; } = new List<FieldType>();
}