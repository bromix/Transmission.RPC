using Transmission.RPC.Messages.TorrentAdd;
using Transmission.RPC.Messages.TorrentGet;
using Transmission.RPC.Messages.TorrentStart;
using Transmission.RPC.Messages.TorrentStop;

namespace Transmission.RPC.Messages;

public static class RequestFactory
{
    public static Request<TRequest> Create<TRequest>(TRequest arguments)
    {
        return arguments switch
        {
            TorrentStartRequestArguments => new Request<TRequest>("torrent-start") { Arguments = arguments },
            TorrentStopRequestArguments => new Request<TRequest>("torrent-stop") { Arguments = arguments },
            TorrentGetRequestArguments => new Request<TRequest>("torrent-get") { Arguments = arguments },
            TorrentAddRequestArguments => new Request<TRequest>("torrent-add") { Arguments = arguments },
            _ => throw new ArgumentOutOfRangeException(nameof(arguments), arguments, null)
        };
    }
}