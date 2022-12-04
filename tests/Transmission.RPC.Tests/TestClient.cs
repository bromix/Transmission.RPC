using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Transmission.DependencyInjection;
using Transmission.RPC.Requests;
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
        TorrentGetRequestArguments arguments = new()
        {
            Fields = new[]
            {
                TorrentGetRequestArguments.Field.Id,
                TorrentGetRequestArguments.Field.HashString,
                TorrentGetRequestArguments.Field.Name,
                TorrentGetRequestArguments.Field.IsPrivate,
                TorrentGetRequestArguments.Field.IsStalled,
                TorrentGetRequestArguments.Field.AddedDate,
                TorrentGetRequestArguments.Field.ActivityDate,
                TorrentGetRequestArguments.Field.TorrentFile
            },
            Ids = new RequestTorrentId[] { 1, "189dbeabefe71534466315bf447fd0e341ffed50" }
        };
        var torrents = await _transmissionRpcClient.TorrentGetAsync(arguments);

        // var torrent = torrents.First();
        // var diff = DateTime.UtcNow - torrent.ActivityDate;
        Assert.True(true);
    }

    [Fact]
    public async Task ClientAdd()
    {
        TorrentAddRequestArguments arguments = new()
        {
            Filename = "https://releases.ubuntu.com/20.04/ubuntu-20.04.3-desktop-amd64.iso.torrent",
            Paused = true
        };

        var result = await _transmissionRpcClient.TorrentAddAsync(arguments);
        Assert.True(true);
    }
}