using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.FreeSpace;

public sealed record FreeSpaceRequest
{
    public FreeSpaceRequest(string path) => Path = path;

    /// <summary>
    /// The directory to query
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; }
}