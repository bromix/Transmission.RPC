using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods;

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

    [JsonPropertyName("arguments")] public TArguments? Arguments { get; init; }
}

internal static class RequestExtensions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public static JsonContent ToJsonContent<TArguments>(this Request<TArguments> request) =>
        JsonContent.Create(request, options: Options);
}