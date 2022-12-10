using Transmission.RPC.Messages.TorrentAdd;
using Transmission.RPC.Messages.TorrentGet;

namespace Transmission.RPC.Messages;

public static class RequestFactory
{
    public static Request<TorrentGetRequestArguments> Create(TorrentGetRequestArguments arguments) =>
        new Request<TorrentGetRequestArguments>("torrent-get") { Arguments = arguments };

    public static Request<TorrentAddRequestArguments> Create(TorrentAddRequestArguments arguments) =>
        new Request<TorrentAddRequestArguments>("torrent-add") { Arguments = arguments };
}