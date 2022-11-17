using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC;

public class FieldTypeJsonConverter : JsonConverter<List<TorrentGetRequestArguments.Field>>
{
    private string toCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLower(input[0]) + input.Substring(1);
    }
    public override List<TorrentGetRequestArguments.Field>? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, List<TorrentGetRequestArguments.Field> value, JsonSerializerOptions options)
    {
        if (value.Count == 0) return;

        writer.WriteStartArray();
        foreach (var fieldType in value)
        {
            // All but a few FieldTypes can be converted to CamelCase.
            string fieldName = fieldType switch
            {
                TorrentGetRequestArguments.Field.FileCount => "file-count",
                TorrentGetRequestArguments.Field.PeerLimit => "peer-limit",
                TorrentGetRequestArguments.Field.PrimaryMimeType => "primary-mime-type",
                _ => toCamelCase(fieldType.ToString())
            };

            writer.WriteStringValue(fieldName);
        }
        writer.WriteEndArray();
    }
}