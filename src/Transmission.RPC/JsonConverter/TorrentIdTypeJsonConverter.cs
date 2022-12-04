using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC.JsonConverter;

internal sealed class TorrentIdTypeJsonConverter : JsonConverter<RequestTorrentId[]>
{
    public override RequestTorrentId[]? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, RequestTorrentId[] value, JsonSerializerOptions options)
    {
        if (value.Length == 0) return;

        writer.WriteStartArray();
        foreach (var torrentId in value)
        {
            switch (torrentId.Id)
            {
                case string hashString:
                    writer.WriteStringValue(hashString);
                    break;
                case int id:
                    writer.WriteNumberValue(id);
                    break;
                default:
                    throw new Exception($"Unsupported TorrentId of type {torrentId.Id.GetType().FullName}");
            }
        }

        writer.WriteEndArray();
    }
}