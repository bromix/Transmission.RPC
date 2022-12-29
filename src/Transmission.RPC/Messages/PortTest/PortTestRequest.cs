namespace Transmission.RPC.Messages.PortTest;

/// <summary>
/// This request tests to see if your incoming peer port is accessible from the outside world.
/// </summary>
[TransmissionMethod("port-test")]
internal sealed record PortTestRequest : ITransmissionRequest;