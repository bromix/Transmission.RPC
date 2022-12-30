namespace Transmission.RPC.Methods.PortTest;

/// <summary>
/// This request tests to see if your incoming peer port is accessible from the outside world.
/// </summary>
[TransmissionMethod("port-test")]
public sealed record PortTestRequest : ITransmissionRequest;