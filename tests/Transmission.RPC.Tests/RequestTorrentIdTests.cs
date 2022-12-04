using FluentAssertions;
using Xunit;

namespace Transmission.RPC.Tests;

public sealed class RequestTorrentIdTests
{
    [Fact]
    public void Assign_Id()
    {
        RequestTorrentId requestTorrentId = 1;
        requestTorrentId.Id.Should().Be(1);
    }

    [Fact]
    public void Assign_HashString()
    {
        RequestTorrentId requestTorrentId = "3485034958345903534050";
        requestTorrentId.Id.Should().Be("3485034958345903534050");
    }
}