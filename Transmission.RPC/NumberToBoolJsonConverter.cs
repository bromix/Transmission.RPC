using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC;

public class NumberToBoolJsonConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number)
            throw new ArgumentException($"Unsupported type {reader.TokenType}");

        return reader.GetInt16() > 0;
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}