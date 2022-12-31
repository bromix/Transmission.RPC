using Transmission.RPC.Methods.FreeSpace;
using Transmission.RPC.Methods.PortTest;
using Transmission.RPC.Methods.TorrentAdd;
using Transmission.RPC.Methods.TorrentGet;
using Transmission.RPC.Methods.TorrentReannounce;
using Transmission.RPC.Methods.TorrentStart;
using Transmission.RPC.Methods.TorrentStartNow;
using Transmission.RPC.Methods.TorrentStop;
using Transmission.RPC.Methods.TorrentVerify;

namespace Transmission.RPC;

public interface ITransmissionRpcClient
{
    /// <summary>
    /// Start a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task StartTorrentAsync(TorrentStartRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Start a torrent now.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task StartTorrentNowAsync(TorrentStartNowRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Stop a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task StopTorrentAsync(TorrentStopRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Verify a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task VerifyTorrentAsync(TorrentVerifyRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Reannounce torrents.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    Task ReannounceTorrentAsync(TorrentReannounceRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns information about torrents.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TorrentGetResult> GetTorrentAsync(TorrentGetRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a torrent.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<TorrentAddResult> AddTorrentAsync(TorrentAddRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// This method tests how much free space is available in a client-specified folder.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<FreeSpaceResult> GetFreeSpaceAsync(FreeSpaceRequest request,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// This method tests to see if your incoming peer port is accessible from the outside world.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<PortTestResult> TestPortAsync(PortTestRequest request,
        CancellationToken cancellationToken = default);
}