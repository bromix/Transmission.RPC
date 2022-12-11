namespace Transmission.RPC.Messages;

public static class ResponseExtensions
{
    public static bool IsSuccess<T>(this Response<T> response) => response.Result.Equals("success");

    public static void ThrowIfUnsuccessful<T>(this Response<T> response)
    {
        if (!response.IsSuccess()) throw new ResponseException(response.Result);
    }
}