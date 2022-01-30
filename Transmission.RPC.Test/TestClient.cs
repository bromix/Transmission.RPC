using System;
using Xunit;

namespace Transmission.RPC.Test;

public class TestClient
{
    [Fact]
    public void Client()
    {
        var username = Environment.GetEnvironmentVariable("TRANSMISSION_USERNAME");
        //var client = new Transmission.RPC.Client();
        Assert.True(true);
    }
}