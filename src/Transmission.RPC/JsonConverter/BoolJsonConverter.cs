using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC.JsonConverter;

public class BoolJsonConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return reader.TokenType switch
        {
            JsonTokenType.Number => NumberToBool(reader),
            JsonTokenType.String => StringToBool(reader),
            _ => throw new ArgumentException($"Unsupported type {reader.TokenType}")
        };
    }

    private static bool StringToBool(Utf8JsonReader reader)
    {
        var value = reader.GetString()?.ToLower() ?? string.Empty;

        return value switch
        {
            "true" => true,
            "false" => false,
            _ => throw new ArgumentException($"Unsupported value {value}")
        };
    }

    private static bool NumberToBool(Utf8JsonReader reader) => reader.GetInt16() > 0;

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}