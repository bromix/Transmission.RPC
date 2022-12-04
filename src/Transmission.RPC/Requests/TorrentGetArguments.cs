using System.Text.Json.Serialization;
using Transmission.RPC.JsonConverter;

namespace Transmission.RPC.Requests;

/// <summary>
/// Method: "torrent-get"
/// </summary>
public sealed class TorrentGetArguments
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

    [JsonPropertyName("ids")]
    [JsonConverter(typeof(TorrentIdTypeJsonConverter))]
    public TorrentId[]? Ids { get; set; }
}