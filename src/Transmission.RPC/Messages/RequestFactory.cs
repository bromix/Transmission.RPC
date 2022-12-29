namespace Transmission.RPC.Messages;

public static class RequestFactory
{
    internal static Request<TRequest> Create<TRequest>(TRequest arguments) where TRequest : ITransmissionRequest
    {
        var messageName = arguments.GetTransmissionMethodName();
        return new Request<TRequest>(messageName) { Arguments = arguments };
    }
}