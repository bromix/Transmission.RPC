using System.Text.Json;

namespace Transmission.RPC.Methods;

internal static class HttpResponseMessageExtensions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task<T> ToResponseAsync<T>(this HttpResponseMessage response,
        CancellationToken cancellationToken = default)
    {
        var jsonStringResponse = await response.Content.ReadAsStringAsync(cancellationToken);
        var unknownResponse = JsonSerializer.Deserialize<T>(jsonStringResponse, Options);

        if (unknownResponse is null)
            throw new InvalidOperationException(
                $"Could not deserialize '{jsonStringResponse}' to type {typeof(T).FullName}");
        return unknownResponse;
    }
}