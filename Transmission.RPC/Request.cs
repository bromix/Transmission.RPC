using System.Text.Json.Serialization;

namespace Transmission.RPC;
public class Request
{
    private static int tagCounter = 0;
    public Request(string method)
    {
        Method = method;
        Tag = ++tagCounter;
    }

    [JsonPropertyName("method")]
    public string Method { get; init; }

    [JsonPropertyName("tag")]
    public int Tag { get; init; }

    [JsonPropertyName("arguments")]
    public object? arguments { get; set; }
}