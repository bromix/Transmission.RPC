using System.Text.Json.Serialization;
using Transmission.RPC.Enums;

namespace Transmission.RPC.Messages.TorrentAdd;

/// <summary>
/// Method: "torrent-add"
/// </summary>
public sealed record TorrentAddRequest
{
    /// <summary>
    /// String of one or more cookies.
    /// </summary>
    [JsonPropertyName("cookies")]
    public string? Cookies { get; set; }

    /// <summary>
    /// Path to download the torrent to.
    /// </summary>
    [JsonPropertyName("download-dir")]
    public string? DownloadDir { get; set; }

    /// <summary>
    /// Filename or URL of the .torrent file.
    /// </summary>
    [JsonPropertyName("filename")]
    public string? Filename { get; set; }

    /// <summary>
    /// Array of string labels
    /// </summary>
    [JsonPropertyName("labels")]
    public string[]? Labels { get; set; }

    /// <summary>
    /// Base64-encoded .torrent content.
    /// </summary>
    [JsonPropertyName("metainfo")]
    public string? Metainfo { get; set; }

    /// <summary>
    /// If true, don't start the torrent.
    /// </summary>
    [JsonPropertyName("paused")]
    public bool? Paused { get; set; }

    /// <summary>
    /// Maximum number of peers
    /// </summary>
    [JsonPropertyName("peer-limit")]
    public int? PeerLimit { get; set; }

    /// <summary>
    /// Torrent's bandwidth priority.
    /// </summary>
    [JsonPropertyName("bandwidthPriority")]
    public Priority? BandwidthPriority { get; set; }
}