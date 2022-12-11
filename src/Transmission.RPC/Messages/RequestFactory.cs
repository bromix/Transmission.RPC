using Transmission.RPC.Messages.PortTest;
using Transmission.RPC.Messages.TorrentAdd;
using Transmission.RPC.Messages.TorrentGet;
using Transmission.RPC.Messages.TorrentReannounce;
using Transmission.RPC.Messages.TorrentStart;
using Transmission.RPC.Messages.TorrentStartNow;
using Transmission.RPC.Messages.TorrentStop;
using Transmission.RPC.Messages.TorrentVerify;

namespace Transmission.RPC.Messages;

public static class RequestFactory
{
    internal static Request<TRequest> Create<TRequest>(TRequest arguments)
    {
        var messageName = arguments switch
        {
            TorrentStartRequest => "torrent-start",
            TorrentStartNowRequest => "torrent-start-now",
            TorrentStopRequest => "torrent-stop",
            TorrentVerifyRequest => "torrent-verify",
            TorrentReannounceRequest => "torrent-reannounce",
            TorrentGetRequest => "torrent-get",
            TorrentAddRequest => "torrent-add",
            PortTestRequest => "port-test",
            _ => throw new ArgumentOutOfRangeException(nameof(arguments), arguments, null)
        };

        return new Request<TRequest>(messageName) { Arguments = arguments };
    }
}