using System.Text.Json.Serialization;

namespace Bromix.Transmission.RPC;

public record Torrent
{
    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? ActivityDate { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? AddedDate { get; init; }
    public int? BandwidthPriority { get; init; }
    public string? Comment { get; init; }
    public int CorruptEver { get; init; }
    public string? Creator { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? DateCreated { get; init; }
    public int DesiredAvailable { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? DoneDate { get; init; }
    public string? DownloadDir { get; init; }
    public int? DownloadedEver { get; init; }
    public int? DownloadLimit { get; init; }
    public bool? DownloadLimited { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? EditDate { get; init; }
    public int? Error { get; init; }
    public string? ErrorString { get; init; }
    public int? Eta { get; init; }
    public int? EtaIdle { get; init; }

    [JsonPropertyName("file-count")]
    public int? FileCount { get; init; }

    // files                       | array(see below)           | n/a
    // fileStats                   | array(see below)           | n/a
    public string? HashString { get; init; }
    public int? HaveUnchecked { get; init; }
    public int? HaveValid { get; init; }
    public bool? HonorsSessionLimits { get; init; }
    public int? Id { get; init; }
    public bool? IsFinished { get; init; }
    public bool? IsPrivate { get; init; }
    public bool? IsStalled { get; init; }

    //labels                      | array(see below)           | tr_torrent

    public int? LeftUntilDone { get; init; }
    public string? MagnetLink { get; init; }
    public int? ManualAnnounceTime { get; init; }
    public int? MaxConnectedPeers { get; init; }
    public double? MetadataPercentComplete { get; init; }
    public string? Name { get; init; }

    [JsonPropertyName("peer-limit")]
    public int? PeerLimit { get; init; }

    //peers                       | array(see below)           | n/a
    public int? PeersConnected { get; init; }
    //peersFrom                   | object (see below)          | n/a

    public int? PeersGettingFromUs { get; init; }
    public int? PeersSendingToUs { get; init; }
    public double? PercentDone { get; init; }
    public string? Pieces { get; init; }
    public int? PieceCount { get; init; }
    public int? PieceSize { get; init; }
    //priorities                  | array(see below)           | n/a
    [JsonPropertyName("primary-mime-type")]
    public string? PrimaryMimeType { get; init; }
    public int? QueuePosition { get; init; }
    public int? RateDownload { get; init; }
    public int? RateUpload { get; init; }
    public double? RecheckProgress { get; init; }
    public int? SecondsDownloading { get; init; }
    public int? SecondsSeeding { get; init; }
    public int? SeedIdleLimit { get; init; }
    public int? SeedIdleMode { get; init; }
    public double? SeedRatioLimit { get; init; }
    public int? SeedRatioMode { get; init; }
    public int? SizeWhenDone { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? StartDate { get; init; }

    //status                      | number(see below)          | tr_stat
    //trackers                    | array(see below)           | n/a
    //trackerStats                | array(see below)           | n/a
    public int? TotalSize { get; init; }
    public string? TorrentFile { get; init; }
    public int? PploadedEver { get; init; }
    public int? UploadLimit { get; init; }
    public bool? UploadLimited { get; init; }
    public double? UploadRatio { get; init; }
    //wanted                      | array(see below)           | n/a
    //webseeds                    | array(see below)           | n/a
    public int? WebseedsSendingToUs { get; init; }
}