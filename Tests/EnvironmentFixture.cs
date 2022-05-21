using System;
using System.IO;

namespace Bromix.Transmission.RPC.Test;

public class EnvironmentFixture : IDisposable
{
    public EnvironmentFixture()
    {
        Bromix.Transmission.RPC.Environment.Load();
    }

    public string TransmissionUrl => System.Environment.GetEnvironmentVariable("TRANSMISSION_URL") ?? throw new InvalidOperationException("TRANSMISSION_URL is missing in Environment Variables.");
    public string TransmissionUserName => System.Environment.GetEnvironmentVariable("TRANSMISSION_USERNAME") ?? throw new InvalidOperationException("TRANSMISSION_USERNAME is missing in Environment Variables.");
    public string TransmissionPassword => System.Environment.GetEnvironmentVariable("TRANSMISSION_PASSWORD") ?? throw new InvalidOperationException("TRANSMISSION_PASSWORD is missing in Environment Variables.");

    public void Dispose()
    {
        // do nothing.
    }
}