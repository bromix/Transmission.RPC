using FluentAssertions;
using Transmission.RPC.Messages;
using Xunit;

namespace Transmission.RPC.Tests.Messages;

public sealed class ExceptionTest
{
    private record MockResponse(int Dummy);

    [Fact]
    public void ThrowIfUnsuccessful()
    {
        var response = new Response<MockResponse> { Result = "Unknown Method" };

        var act = () => response.ThrowIfUnsuccessful();
        act.Should().Throw<ResponseException>().WithMessage("Unknown Method");
    }
}