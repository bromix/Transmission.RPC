using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Transmission.DependencyInjection;
using Transmission.RPC.Messages;
using Transmission.RPC.Messages.TorrentAdd;
using Transmission.RPC.Messages.TorrentGet;
using Transmission.RPC.Messages.TorrentStart;
using Transmission.RPC.Messages.TorrentStop;
using Xunit;

namespace Transmission.RPC.Tests;

public sealed class TestClient : IClassFixture<EnvFile>
{
    private readonly TransmissionRpcClient _transmissionRpcClient;

    public TestClient(EnvFile envFile)
    {
        var serviceProvider = new ServiceCollection()
            .AddTransmissionRpcClient(provider =>
            {
                TransmissionRpcClientOptions options = new
                (
                    Url: new Uri(envFile.TransmissionUrl),
                    Username: envFile.TransmissionUserName,
                    Password: envFile.TransmissionPassword
                );
                return options;
            })
            .BuildServiceProvider();

        _transmissionRpcClient = serviceProvider.GetRequiredService<TransmissionRpcClient>();
        // _transmissionRpcClient = new TransmissionRpcClient
        // (
        //     envFile.TransmissionUrl,
        //     envFile.TransmissionUserName,
        //     envFile.TransmissionPassword
        // );
    }

    [Fact]
    public async Task ClientGet()
    {
        TorrentGetRequestArguments requestArguments = new()
        {
            Fields = new[]
            {
                TorrentGetRequestArguments.Field.Id,
                TorrentGetRequestArguments.Field.HashString,
                TorrentGetRequestArguments.Field.Name,
                TorrentGetRequestArguments.Field.Files,
                TorrentGetRequestArguments.Field.Status,
                TorrentGetRequestArguments.Field.FileStats,
                TorrentGetRequestArguments.Field.FileCount,
                TorrentGetRequestArguments.Field.IsPrivate,
                TorrentGetRequestArguments.Field.IsStalled,
                TorrentGetRequestArguments.Field.AddedDate,
                TorrentGetRequestArguments.Field.ActivityDate,
                TorrentGetRequestArguments.Field.TorrentFile
            }
            //Ids = new TorrentId[] { 1, "189dbeabefe71534466315bf447fd0e341ffed50" }
        };
        var torrents = await _transmissionRpcClient.TorrentGetAsync(requestArguments);

        // var torrent = torrents.First();
        // var diff = DateTime.UtcNow - torrent.ActivityDate;
        Assert.True(true);
    }

    [Fact]
    public async Task ClientAdd()
    {
        TorrentAddRequestArguments requestArguments = new()
        {
            Filename = "https://releases.ubuntu.com/20.04/ubuntu-20.04.3-desktop-amd64.iso.torrent",
            Paused = true
        };

        var result = await _transmissionRpcClient.TorrentAddAsync(requestArguments);
        Assert.True(true);
    }

    [Fact]
    public async Task TorrentStart()
    {
        TorrentStartRequestArguments arguments = new()
            { Ids = new TorrentId[] { "a305900fb229d3fa3b1b0c10ac0584b2748099fe" } };

        var result = await _transmissionRpcClient.TorrentStartAsync(arguments);
        Assert.True(true);
    }
    
    [Fact]
    public async Task TorrentStop()
    {
        TorrentStopRequestArguments arguments = new()
            { Ids = new TorrentId[] { "a305900fb229d3fa3b1b0c10ac0584b2748099fe" } };

        var result = await _transmissionRpcClient.TorrentStopAsync(arguments);
        Assert.True(true);
    }
}