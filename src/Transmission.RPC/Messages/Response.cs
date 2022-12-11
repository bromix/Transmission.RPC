namespace Transmission.RPC.Messages;

internal sealed record Response<TArguments>
{
    public string Result { get; init; } = string.Empty;
    public TArguments Arguments { get; init; } = default!;
    public int? Tag { get; init; } = null;
}