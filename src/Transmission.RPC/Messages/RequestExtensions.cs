using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC.Messages;

internal static class RequestExtensions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public static JsonContent ToJsonContent<TArguments>(this Request<TArguments> request) =>
        JsonContent.Create(request, options: Options);
}