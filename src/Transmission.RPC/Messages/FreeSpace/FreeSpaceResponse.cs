using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages.FreeSpace;

public sealed record FreeSpaceResponse
{
    /// <summary>
    /// Same as the Request argument
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; } = string.Empty;

    /// <summary>
    /// The size, in bytes, of the free space in that directory.
    /// </summary>
    [JsonPropertyName("size-bytes")]
    public long SizeBytes { get; } = 0;

    /// <summary>
    /// The total capacity, in bytes, of that directory.
    /// </summary>
    [JsonPropertyName("total_size")]
    public long TotalSize { get; } = 0;
}