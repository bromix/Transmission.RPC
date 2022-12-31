using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Transmission.DependencyInjection;
using Transmission.RPC.Enums;
using Transmission.RPC.Methods;
using Transmission.RPC.Methods.FreeSpace;
using Transmission.RPC.Methods.PortTest;
using Transmission.RPC.Methods.TorrentAdd;
using Transmission.RPC.Methods.TorrentGet;
using Transmission.RPC.Methods.TorrentStart;
using Transmission.RPC.Methods.TorrentStop;
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
                ClientOptions options = new
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
        TorrentGetRequest request = new(new[]
        {
            TorrentGetRequestField.Id,
            TorrentGetRequestField.HashString,
            TorrentGetRequestField.Name,
            TorrentGetRequestField.BandwidthPriority,
            // TorrentGetRequestField.Files,
            // TorrentGetRequestField.Status,
            // TorrentGetRequestField.FileStats,
            // TorrentGetRequestField.FileCount,
            // TorrentGetRequestField.IsPrivate,
            // TorrentGetRequestField.IsStalled,
            // TorrentGetRequestField.AddedDate,
            // TorrentGetRequestField.ActivityDate,
            // TorrentGetRequestField.TorrentFile
        });

        var response = await _transmissionRpcClient.GetTorrentAsync(request);
        var x = 0;
    }

    [Fact]
    public async Task ClientAdd()
    {
        TorrentAddRequest request = new()
        {
            Filename = "https://releases.ubuntu.com/22.04/ubuntu-22.04.1-live-server-amd64.iso.torrent",
            BandwidthPriority = Priority.Low,
            Paused = true
        };

        var result = await _transmissionRpcClient.AddTorrentAsync(request);
        var x = 0;
    }

    [Fact]
    public async Task TorrentStart()
    {
        TorrentStartRequest arguments = new()
            { Ids = new TorrentId[] { "a305900fb229d3fa3b1b0c10ac0584b2748099fe" } };

        await _transmissionRpcClient.StartTorrentAsync(arguments);
        Assert.True(true);
    }

    [Fact]
    public async Task TorrentStop()
    {
        TorrentStopRequest arguments = new() { Ids = new TorrentId[] { "a305900fb229d3fa3b1b0c10ac0584b2748099fe" } };
        await _transmissionRpcClient.StopTorrentAsync(arguments);
    }

    [Fact]
    public async Task PortTest()
    {
        var response = await _transmissionRpcClient.TestPortAsync(new PortTestRequest());
        response.PortIsOpen.Should().BeTrue();
    }

    [Fact]
    public async Task FreeSpaceTest()
    {
        var response = await _transmissionRpcClient.GetFreeSpaceAsync(new FreeSpaceRequest("/media/torrents"));
    }
}