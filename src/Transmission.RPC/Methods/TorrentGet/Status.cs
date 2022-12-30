namespace Transmission.RPC.Methods.TorrentGet;

public enum Status
{
    Stopped = 0,
    QueuedToVerifyLocalData = 1,
    TorrentIsVerifyingLocalData = 2,
    TorrentIsQueuedToDownload = 3,
    TorrentIsDownloading = 4,
    TorrentIsQueuedToSeed = 5,
    TorrentIsSeeding = 6
}