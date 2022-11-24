using System.Collections.Generic;
using System.Threading.Tasks;
using Transmission.RPC;
using Xunit;

namespace Transmission.RPC.Tests;

public class TestClient : IClassFixture<EnvironmentFixture>
{
    private EnvironmentFixture _environmentFixture;
    private Client _client;

    public TestClient(EnvironmentFixture environmentFixture)
    {
        this._environmentFixture = environmentFixture;

        this._client = new global::Transmission.RPC.Client
        (
            environmentFixture.TransmissionUrl,
            environmentFixture.TransmissionUserName,
            environmentFixture.TransmissionPassword
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