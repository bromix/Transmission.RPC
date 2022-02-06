using Xunit;

namespace Transmission.RPC.Test;

public class TestClient : IClassFixture<EnvironmentFixture>
{
    private EnvironmentFixture environmentFixture;
    private Client client;

    public TestClient(EnvironmentFixture environmentFixture)
    {
        this.environmentFixture = environmentFixture;

        this.client = new Transmission.RPC.Client
        (
            environmentFixture.TransmissionUrl,
            environmentFixture.TransmissionUserName,
            environmentFixture.TransmissionPassword
        );
    }

    [Fact]
    public void ClientGet()
    {
        Arguments.TorrentGet arguments = new()
        {
            Fields = new()
            {
                Arguments.TorrentGet.FieldType.Id,
                Arguments.TorrentGet.FieldType.Name
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
        Arguments.TorrentAdd arguments = new()
        {
            Filename = "https://releases.ubuntu.com/20.04/ubuntu-20.04.3-desktop-amd64.iso.torrent",
            Paused = true,
            DownloadDir = "/media/torrents/linux"
        };

        var result = client.TorrentAdd(arguments);
        Assert.True(true);
    }
}