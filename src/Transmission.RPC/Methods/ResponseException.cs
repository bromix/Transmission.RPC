namespace Transmission.RPC.Methods;

public sealed class ResponseException: Exception
{
    public ResponseException(string? message) : base(message)
    {
    }
}