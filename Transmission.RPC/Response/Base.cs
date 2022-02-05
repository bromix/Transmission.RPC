using System.Text.Json.Serialization;

namespace Transmission.RPC.Response;

public class Base<TArguments>
{
    public string Result { get; init; } = default!;
    public TArguments? Arguments { get; init; }
    public int? Tag { get; init; }
}
