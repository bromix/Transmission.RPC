using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.FreeSpace;

public sealed record FreeSpaceResult
{
    /// <summary>
    /// Same as the Request argument
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; init; } = string.Empty;

    /// <summary>
    /// The size, in bytes, of the free space in that directory.
    /// </summary>
    [JsonPropertyName("size-bytes")]
    public long SizeBytes { get; init; } = 0;

    /// <summary>
    /// The total capacity, in bytes, of that directory.
    /// </summary>
    [JsonPropertyName("total_size")]
    public long TotalSize { get; init; } = 0;
}