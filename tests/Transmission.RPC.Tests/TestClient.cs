using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Transmission.RPC.Tests;

public sealed class TestClient : IClassFixture<EnvFile>
{
    private readonly Client _client;

    public TestClient(EnvFile envFile)
    {
        _client = new Client
        (
            envFile.TransmissionUrl,
            envFile.TransmissionUserName,
            envFile.TransmissionPassword
        );
    }

    [Fact]
    public async Task ClientGet()
    {
        TorrentGetRequestArguments arguments = new()
        {
            Fields = new List<TorrentGetRequestArguments.Field>
            {
                TorrentGetRequestArguments.Field.Id,
                TorrentGetRequestArguments.Field.Name,
                TorrentGetRequestArguments.Field.IsPrivate,
                TorrentGetRequestArguments.Field.IsStalled,
                TorrentGetRequestArguments.Field.AddedDate,
                TorrentGetRequestArguments.Field.ActivityDate,
                TorrentGetRequestArguments.Field.TorrentFile
            }
        };
        var torrents = await _client.TorrentGetAsync(arguments);

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

        var result = await _client.TorrentAddAsync(arguments);
        Assert.True(true);
    }
}