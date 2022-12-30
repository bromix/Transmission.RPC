namespace Transmission.RPC.Methods;

internal sealed record Response<TArguments>
{
    public string Result { get; init; } = string.Empty;
    public TArguments Arguments { get; init; } = default!;
    public int? Tag { get; init; } = null;
}

internal static class ResponseExtensions
{
    private static bool IsSuccess<T>(this Response<T> response) => response.Result.Equals("success");

    internal static void ThrowIfUnsuccessful<T>(this Response<T> response)
    {
        if (!response.IsSuccess()) throw new ResponseException(response.Result);
    }
}