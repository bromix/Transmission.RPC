using System.Text.Json.Serialization;

namespace Transmission.RPC.Arguments;

public class TorrentGet
    {
        public enum FieldType
        {
            Id,
            Name
        };

        [JsonPropertyName("fields")]
        [JsonConverter(typeof(FieldTypeJsonConverter))]
        public List<FieldType> Fields { get; set; } = new List<FieldType>();
    }