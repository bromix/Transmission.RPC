using System.Text.Json;
using System.Text.Json.Serialization;

namespace Transmission.RPC.Methods.TorrentGet;

public class DateTimeJsonConverter : JsonConverter<DateTime?>
{
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.Number)
            throw new ArgumentException($"Unsupported type {reader.TokenType}");

        if(reader.GetInt64() == 0) return null;
        return DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).UtcDateTime;
    }

    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}