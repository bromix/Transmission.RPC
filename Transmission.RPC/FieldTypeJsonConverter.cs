using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC;

public class FieldTypeJsonConverter : JsonConverter<List<TorrentGetRequestArguments.FieldType>>
{
    public override List<TorrentGetRequestArguments.FieldType>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<TorrentGetRequestArguments.FieldType> value, JsonSerializerOptions options)
    {
        if (value.Count == 0) return;

        writer.WriteStartArray();
        foreach (var fieldType in value)
        {
            string fieldName = fieldType switch
            {
                TorrentGetRequestArguments.FieldType.Id => "id",
                TorrentGetRequestArguments.FieldType.Name => "name",
                _ => throw new ArgumentException($"Unknown method {value}."),
            };

            writer.WriteStringValue(fieldName);
        }
        writer.WriteEndArray();
    }
}