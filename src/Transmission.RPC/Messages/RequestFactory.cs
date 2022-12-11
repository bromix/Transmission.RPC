﻿using Transmission.RPC.Messages.TorrentAdd;
using Transmission.RPC.Messages.TorrentGet;
using Transmission.RPC.Messages.TorrentStart;
using Transmission.RPC.Messages.TorrentStartNow;
using Transmission.RPC.Messages.TorrentStop;
using Transmission.RPC.Messages.TorrentVerify;

namespace Transmission.RPC.Messages;

public static class RequestFactory
{
    public static Request<TRequest> Create<TRequest>(TRequest arguments)
    {
        var messageName = arguments switch
        {
            TorrentStartRequest => "torrent-start",
            TorrentStartNowRequest => "torrent-start-now",
            TorrentStopRequest => "torrent-stop",
            TorrentVerifyRequest => "torrent-verify",
            TorrentGetRequest => "torrent-get",
            TorrentAddRequest => "torrent-add",
            _ => throw new ArgumentOutOfRangeException(nameof(arguments), arguments, null)
        };

        return new Request<TRequest>(messageName) { Arguments = arguments };
    }
}