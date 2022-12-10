using FluentAssertions;
using Transmission.RPC.Messages;
using Xunit;

namespace Transmission.RPC.Tests.Requests;

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
}