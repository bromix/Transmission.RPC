namespace Transmission.RPC.Methods;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class TransmissionMethodAttribute : Attribute
{
    public TransmissionMethodAttribute(string methodName) => MethodName = methodName;
    public string MethodName { get; }
}

internal static class TransmissionMethodAttributeExtension
{
    internal static string GetTransmissionMethodName(this ITransmissionRequest request)
    {
        var type = request.GetType();
        var attribute =
            Attribute.GetCustomAttribute(type, typeof(TransmissionMethodAttribute)) as TransmissionMethodAttribute;
        return attribute?.MethodName ?? string.Empty;
    }
}