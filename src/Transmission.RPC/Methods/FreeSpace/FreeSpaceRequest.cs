using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.FreeSpace;

[TransmissionMethod("free-space")]
public sealed record FreeSpaceRequest : ITransmissionRequest
{
    public FreeSpaceRequest(string path) => Path = path;

    /// <summary>
    /// The directory to query
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; }
}