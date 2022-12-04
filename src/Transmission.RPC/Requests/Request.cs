using System.Text.Json.Serialization;

namespace Transmission.RPC.Requests;

public sealed record Request
{
    private static int _tagCounter;

    public Request(string method)
    {
        Method = method;
        Tag = ++_tagCounter;
    }

    [JsonPropertyName("method")] public string Method { get; }

    [JsonPropertyName("tag")] public int? Tag { get; }

    [JsonPropertyName("arguments")] public object? Arguments { get; set; }
}