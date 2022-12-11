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
            TorrentStartRequest => new Request<TRequest>("torrent-start") { Arguments = arguments },
            TorrentStopRequest => new Request<TRequest>("torrent-stop") { Arguments = arguments },
            TorrentGetRequest => new Request<TRequest>("torrent-get") { Arguments = arguments },
            TorrentAddRequest => new Request<TRequest>("torrent-add") { Arguments = arguments },
            _ => throw new ArgumentOutOfRangeException(nameof(arguments), arguments, null)
        };
    }
}