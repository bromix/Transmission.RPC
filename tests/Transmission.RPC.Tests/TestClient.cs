using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Transmission.DependencyInjection;
using Transmission.RPC.Enums;
using Transmission.RPC.Messages;
using Transmission.RPC.Messages.TorrentAdd;
using Transmission.RPC.Messages.TorrentGet;
using Transmission.RPC.Messages.TorrentStart;
using Transmission.RPC.Messages.TorrentStop;
using Xunit;

namespace Transmission.RPC.Tests;

public sealed class TestClient : IClassFixture<EnvFile>
{
    private readonly Client _client;

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

        _client = serviceProvider.GetRequiredService<Client>();
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
        TorrentGetRequest request = new()
        {
            Fields = new[]
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
            }
            //Ids = new TorrentId[] { 1, "189dbeabefe71534466315bf447fd0e341ffed50" }
        };

        var response = await _client.TorrentGetAsync(request);
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

        var result = await _client.TorrentAddAsync(request);
        var x = 0;
    }

    [Fact]
    public async Task TorrentStart()
    {
        TorrentStartRequest arguments = new()
            { Ids = new TorrentId[] { "a305900fb229d3fa3b1b0c10ac0584b2748099fe" } };

        await _client.TorrentStartAsync(arguments);
        Assert.True(true);
    }

    [Fact]
    public async Task TorrentStop()
    {
        TorrentStopRequest arguments = new() { Ids = new TorrentId[] { "a305900fb229d3fa3b1b0c10ac0584b2748099fe" } };

        await _client.TorrentStopAsync(arguments);
    }

    [Fact]
    public async Task PortTest()
    {
        var response = await _client.PortTestAsync();
        response.PortIsOpen.Should().BeTrue();
    }
}