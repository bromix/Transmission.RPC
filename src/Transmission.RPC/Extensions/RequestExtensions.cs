using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Transmission.RPC.Requests;

namespace Transmission.RPC.Extensions;

internal static class RequestExtensions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    public static JsonContent ToJsonContent<TArguments>(this Request<TArguments> request) =>
        JsonContent.Create(request, options: Options);
}