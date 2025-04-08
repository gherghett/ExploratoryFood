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
        if( open >= close)
        {
            throw new ArgumentException("OpenHoursEntry cannot open before or at the same thime it closes");
        }
        Open = open;
        Close = close;
    }
}
