namespace Transmission.RPC.Messages;

public sealed class ResponseException: Exception
{
    public ResponseException(string? message) : base(message)
    {
    }
}