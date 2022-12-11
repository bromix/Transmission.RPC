using FluentAssertions;
using Transmission.RPC.Messages;
using Xunit;

namespace Transmission.RPC.Tests.Messages;

public sealed class TorrentIdTests
{
    [Fact]
    public void Assign_Id()
    {
        TorrentId torrentId = 1;
        torrentId.Id.Should().Be(1);
    }

    [Fact]
    public void Assign_HashString()
    {
        TorrentId torrentId = "3485034958345903534050";
        torrentId.Id.Should().Be("3485034958345903534050");
    }

    [Fact]
    public void Is_Equal()
    {
        TorrentId torrentId1 = 1;
        TorrentId torrentId2 = 1;
        torrentId1.Should().Be(torrentId1);
        torrentId1.Should().Be(torrentId2);
    }

    [Fact]
    public void Is_NotEqual()
    {
        TorrentId torrentId1 = 1;
        TorrentId torrentId2 = 2;
        torrentId1.Should().NotBe(torrentId2);
    }
}