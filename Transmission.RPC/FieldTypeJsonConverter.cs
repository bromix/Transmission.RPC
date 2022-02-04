using System.Text.Json;
using System.Text.Json.Serialization;
using static Transmission.RPC.Client;

namespace Transmission.RPC;

public class FieldTypeJsonConverter : JsonConverter<List<Arguments.TorrentGet.FieldType>>
{
    public override List<Arguments.TorrentGet.FieldType>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<Arguments.TorrentGet.FieldType> value, JsonSerializerOptions options)
    {
        if (value.Count == 0) return;

        writer.WriteStartArray();
        foreach (var fieldType in value)
        {
            string fieldName = fieldType switch
            {
                Arguments.TorrentGet.FieldType.Id => "id",
                Arguments.TorrentGet.FieldType.Name => "name",
                _ => throw new ArgumentException($"Unknown method {value}."),
            };

            writer.WriteStringValue(fieldName);
        }
        writer.WriteEndArray();
    }
}