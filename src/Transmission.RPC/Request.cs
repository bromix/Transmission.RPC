using System.Text.Json.Serialization;

namespace Transmission.RPC;

public sealed record Request
{
    private static int _tagCounter;

    public Request(string method)
    {
        Method = method;
        Tag = ++_tagCounter;
    }

    [JsonPropertyName("method")] public string Method { get; init; }

    [JsonPropertyName("tag")] public int Tag { get; init; }

    [JsonPropertyName("arguments")] public object? Arguments { get; set; }
}

/// <summary>
/// Method: "torrent-add"
/// </summary>
public sealed record TorrentAddRequestArguments
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
    public string? Metainfo { get; set; }

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
public sealed class TorrentGetRequestArguments
{
    public enum Field
    {
        ActivityDate,
        AddedDate,
        BandwidthPriority,
        Comment,
        CorruptEver,
        Creator,
        DateCreated,
        DesiredAvailable,
        DoneDate,
        DownloadDir,
        DownloadedEver,
        DownloadLimit,
        DownloadLimited,
        EditDate,
        Error,
        ErrorString,
        Eta,
        EtaIdle,
        FileCount,
        Files,
        FileStats,
        HashString,
        HaveUnchecked,
        HaveValid,
        HonorsSessionLimits,
        Id,
        IsFinished,
        IsPrivate,
        IsStalled,
        Labels,
        LeftUntilDone,
        MagnetLink,
        ManualAnnounceTime,
        MaxConnectedPeers,
        MetadataPercentComplete,
        Name,
        PeerLimit,
        Peers,
        PeersConnected,
        PeersFrom,
        PeersGettingFromUs,
        PeersSendingToUs,
        PercentDone,
        Pieces,
        PieceCount,
        PieceSize,
        Priorities,
        PrimaryMimeType,
        QueuePosition,
        RateDownload,
        RateUpload,
        RecheckProgress,
        SecondsDownloading,
        SecondsSeeding,
        SeedIdleLimit,
        SeedIdleMode,
        SeedRatioLimit,
        SeedRatioMode,
        SizeWhenDone,
        StartDate,
        Status,
        Trackers,
        TrackerStats,
        TotalSize,
        TorrentFile,
        UploadedEver,
        UploadLimit,
        UploadLimited,
        UploadRatio,
        Wanted,
        Webseeds,
        WebseedsSendingToUs,
    }

    [JsonPropertyName("fields")]
    [JsonConverter(typeof(FieldTypeJsonConverter))]
    public Field[] Fields { get; set; } = Array.Empty<Field>();
}