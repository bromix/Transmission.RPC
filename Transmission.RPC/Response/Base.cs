using System.Text.Json.Serialization;

namespace Transmission.RPC.Response;

public class Base
{
    [JsonPropertyName("result")]
    public string Result { get; init; } = default!;
}
