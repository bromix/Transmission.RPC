using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.TorrentGet;

internal class TorrentGetRequestFieldTypeJsonConverter : JsonConverter<TorrentGetRequestField[]>
{
    private static string ToCamelCase(string input)
    {
        if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            return input;

        return char.ToLower(input[0]) + input[1..];
    }

    public override TorrentGetRequestField[]? Read(ref Utf8JsonReader reader, Type typeToConvert,
        JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, TorrentGetRequestField[] value,
        JsonSerializerOptions options)
    {
        if (value.Length == 0) return;

        writer.WriteStartArray();
        foreach (var fieldType in value)
        {
            // All but a few FieldTypes can be converted to CamelCase.
            var fieldName = fieldType switch
            {
                TorrentGetRequestField.FileCount => "file-count",
                TorrentGetRequestField.PeerLimit => "peer-limit",
                TorrentGetRequestField.PrimaryMimeType => "primary-mime-type",
                _ => ToCamelCase(fieldType.ToString())
            };

            writer.WriteStringValue(fieldName);
        }

        writer.WriteEndArray();
    }
}