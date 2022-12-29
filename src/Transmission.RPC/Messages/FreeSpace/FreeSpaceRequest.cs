using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.FreeSpace;

[TransmissionMethod("free-space")]
public record FreeSpaceRequest: ITransmissionRequest
{
    public FreeSpaceRequest(string path) => Path = path;

    /// <summary>
    /// The directory to query
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; }
}