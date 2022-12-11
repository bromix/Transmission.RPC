using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages;

internal sealed record Request<TArguments>
{
    private static int _tagCounter;

    public Request(string method)
    {
        Method = method;
        Tag = ++_tagCounter;
    }

    [JsonPropertyName("method")] public string Method { get; }

    [JsonPropertyName("tag")] public int? Tag { get; }

    [JsonPropertyName("arguments")] public TArguments? Arguments { get; set; }
}