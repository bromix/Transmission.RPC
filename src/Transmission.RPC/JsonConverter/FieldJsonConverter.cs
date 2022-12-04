using System.Text.Json;
using System.Text.Json.Serialization;
using Transmission.RPC.Requests;

namespace Transmission.RPC.JsonConverter;

internal class FieldTypeJsonConverter : JsonConverter<TorrentGetArguments.Field[]>
{
    private static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLower(input[0]) + input[1..];
    }

    public override TorrentGetArguments.Field[]? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TorrentGetArguments.Field[] value,
        JsonSerializerOptions options)
    {
        if (value.Length == 0) return;

        writer.WriteStartArray();
        foreach (var fieldType in value)
        {
            // All but a few FieldTypes can be converted to CamelCase.
            var fieldName = fieldType switch
            {
                TorrentGetArguments.Field.FileCount => "file-count",
                TorrentGetArguments.Field.PeerLimit => "peer-limit",
                TorrentGetArguments.Field.PrimaryMimeType => "primary-mime-type",
                _ => ToCamelCase(fieldType.ToString())
            };

            writer.WriteStringValue(fieldName);
        }

        writer.WriteEndArray();
    }
}