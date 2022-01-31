namespace Transmission.RPC;

public class Response
{
    public Arguments? Arguments { get; set; }
}

public class Arguments
{
    public Torrent[]? Torrents { get; set; }
}