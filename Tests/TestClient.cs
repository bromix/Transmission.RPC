using Xunit;

namespace Bromix.Transmission.RPC.Test;

public class TestClient : IClassFixture<EnvironmentFixture>
{
    private EnvironmentFixture environmentFixture;
    private Client client;

    public TestClient(EnvironmentFixture environmentFixture)
    {
        this.environmentFixture = environmentFixture;

        this.client = new Bromix.Transmission.RPC.Client
        (
            environmentFixture.TransmissionUrl,
            environmentFixture.TransmissionUserName,
            environmentFixture.TransmissionPassword
        );
    }

    [Fact]
    public void ClientGet()
    {
        TorrentGetRequestArguments arguments = new()
        {
            Fields = new()
            {
                TorrentGetRequestArguments.Field.Id,
                TorrentGetRequestArguments.Field.Name,
                TorrentGetRequestArguments.Field.IsPrivate,
                TorrentGetRequestArguments.Field.IsStalled,
                TorrentGetRequestArguments.Field.AddedDate,
                TorrentGetRequestArguments.Field.TorrentFile
            }
        };
        var torrents = client.TorrentGet(arguments);

        // var torrent = torrents.First();
        // var diff = DateTime.UtcNow - torrent.ActivityDate;
        Assert.True(true);
    }

    [Fact]
    public void ClientAdd()
    {
        TorrentAddRequestArguments arguments = new()
        {
            Filename = "https://releases.ubuntu.com/20.04/ubuntu-20.04.3-desktop-amd64.iso.torrent",
            Paused = true
        };

        var result = client.TorrentAdd(arguments);
        Assert.True(true);
    }
}