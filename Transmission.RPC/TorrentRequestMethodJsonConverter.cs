using System.Text.Json;
using System.Text.Json.Serialization;
using static Transmission.RPC.Client;

namespace Transmission.RPC;

public class TorrentRequestMethodJsonConverter : JsonConverter<RequestMethod>
{
    public override RequestMethod Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, RequestMethod value, JsonSerializerOptions options)
    {
        string method = value switch
        {
            RequestMethod.TorrentGet => "torrent-get",
            RequestMethod.TorrentAdd => "torrent-add",
            _ => throw new ArgumentException($"Unknown method {value}."),
        };

        writer.WriteStringValue(method);
    }
}