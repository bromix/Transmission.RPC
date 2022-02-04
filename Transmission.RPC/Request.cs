using System.Text.Json.Serialization;

namespace Transmission.RPC;
public class Request
{
    public Request(string method)
    {
        Method = method;
    }

    [JsonPropertyName("method")]
    public string Method { get; init; }

    [JsonPropertyName("arguments")]
    public object? arguments { get; set; }
}