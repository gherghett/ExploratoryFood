using System.Text.Json.Serialization;

namespace Food.Core.Model;
public class OpenHourEntry
{
    [JsonConverter(typeof(TimeOnlyJsonConverter))]

    public TimeOnly Open { get; set; }

    [JsonConverter(typeof(TimeOnlyJsonConverter))]
    public TimeOnly Close { get; set; }

    public OpenHourEntry() { } // Required for deserialization

    public OpenHourEntry(TimeOnly open, TimeOnly close)
    {
        Open = open;
        Close = close;
    }
}
