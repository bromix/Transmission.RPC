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

    public static JsonContent ToJsonContent(this Request request) => JsonContent.Create(request, options: Options);
}