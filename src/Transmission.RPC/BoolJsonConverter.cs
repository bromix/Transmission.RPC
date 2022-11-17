using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC;

public class BoolJsonConverter : JsonConverter<bool>
{
    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
            return reader.GetInt16() > 0;

        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString()?.ToLower();
            if (!String.IsNullOrWhiteSpace(value))
            {
                if (value == "true") return true;
                if (value == "false") return false;
            }
        }

        throw new ArgumentException($"Unsupported type {reader.TokenType}");
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}