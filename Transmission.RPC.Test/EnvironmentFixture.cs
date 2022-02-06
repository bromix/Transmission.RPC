using System;
using System.IO;

namespace Transmission.RPC.Test;

public class EnvironmentFixture : IDisposable
{
    public EnvironmentFixture()
    {
        var pathOfTest = AppDomain.CurrentDomain.BaseDirectory;
        var envFileName = Path.Combine(pathOfTest, ".env");
        if (!File.Exists(envFileName)) return;
        foreach (string line in System.IO.File.ReadLines(envFileName))
        {
            var values = line.Split("=");
            if (values.Length != 2) continue;
            Environment.SetEnvironmentVariable(values[0], values[1]);
        }
    }

    public string TransmissionUrl => Environment.GetEnvironmentVariable("TRANSMISSION_URL") ?? throw new InvalidOperationException("TRANSMISSION_URL is missing in Environment Variables.");
    public string TransmissionUserName => Environment.GetEnvironmentVariable("TRANSMISSION_USERNAME") ?? throw new InvalidOperationException("TRANSMISSION_USERNAME is missing in Environment Variables.");
    public string TransmissionPassword => Environment.GetEnvironmentVariable("TRANSMISSION_PASSWORD") ?? throw new InvalidOperationException("TRANSMISSION_PASSWORD is missing in Environment Variables.");

    public void Dispose()
    {
        // do nothing.
    }
}