using System;
using System.Collections.Generic;
using System.IO;

namespace Transmission.RPC.Tests;

public sealed class EnvFile
{
    public EnvFile()
    {
        var pathOfTest = AppDomain.CurrentDomain.BaseDirectory;
        var envFileName = Path.Combine(pathOfTest, ".env");
        if (!File.Exists(envFileName)) return;
        foreach (var line in File.ReadLines(envFileName))
        {
            var index = line.IndexOf("=", StringComparison.Ordinal);
            if (index <= 0) continue;

            var key = line[..index];
            var value = line[(index + 1)..];
            _values.Add(key, value);
        }
    }

    public string TransmissionUrl => _values["TRANSMISSION_URL"];
    public string TransmissionUserName => _values["TRANSMISSION_USERNAME"];
    public string TransmissionPassword => _values["TRANSMISSION_PASSWORD"];

    private readonly Dictionary<string, string> _values = new();
}