using System.Text.Json.Serialization;

namespace Transmission.RPC;

public class Torrent
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? HashString { get; init; }

    public string? DownloadDir { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? ActivityDate { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? AddedDate { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? DateCreated { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? DoneDate { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? EditDate { get; init; }

    [JsonConverter(typeof(DateTimeJsonConverter))]
    public DateTime? StartDate { get; init; }

    [JsonPropertyName("dht-enabled")]
    [JsonConverter(typeof(BoolJsonConverter))]
    public bool? DhtEnabled { get; init; }

    [JsonPropertyName("pex-enabled")]
    [JsonConverter(typeof(BoolJsonConverter))]
    public bool? PexEnabled { get; init; }

    public int? QueuePosition { get; init; }
}