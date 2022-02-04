using System.Text.Json.Serialization;

namespace Transmission.RPC.Arguments;
public class TorrentAdd
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