namespace Transmission.RPC.Methods;

public static class ResponseExtensions
{
    internal static bool IsSuccess<T>(this Response<T> response) => response.Result.Equals("success");

    internal static void ThrowIfUnsuccessful<T>(this Response<T> response)
    {
        if (!response.IsSuccess()) throw new ResponseException(response.Result);
    }
}