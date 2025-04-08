
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Food.Core.Model;

// Value Object for Open Hours
[JsonConverter(typeof(OpenHoursJsonConverter))]
public class OpenHours
{
    private Dictionary<DayOfWeek, OpenHourEntry> _hours;
    private IClock _clock = new SystemClock();
    public IReadOnlyDictionary<DayOfWeek, OpenHourEntry> Hours => _hours;

    // EF Core requires a parameterless constructor
    private OpenHours() => _hours = new Dictionary<DayOfWeek,OpenHourEntry>();

    public OpenHours(Dictionary<DayOfWeek, OpenHourEntry> hours, IClock clock = null!)
    {
        _hours = new Dictionary<DayOfWeek, OpenHourEntry>(hours);
        _clock = clock ?? new SystemClock();
    }

    // ✅ Explicitly tell System.Text.Json how to deserialize this object
    [JsonConstructor]
    public OpenHours(Dictionary<DayOfWeek, OpenHourEntry> hours)
    {
        _hours = new Dictionary<DayOfWeek, OpenHourEntry>(hours);
    }


    // Method to update hours while maintaining immutability
    public OpenHours UpdateHours(DayOfWeek day, OpenHourEntry entry)
    {
        var updatedHours = new Dictionary<DayOfWeek, OpenHourEntry>(_hours)
        {
            [day] = entry
        };

        return new OpenHours(updatedHours);
    }

    // Convert to JSON for database storage
    public string ToJson()
    {
        return JsonSerializer.Serialize(_hours,new JsonSerializerOptions
        {
            WriteIndented = false,
            PropertyNameCaseInsensitive = true
        });
    }

    internal bool IsOpenForOrders()
    {
        var today = _clock.Today;
        var time = _clock.CurrentTime;
        if( !_hours.TryGetValue(today, out var hours))
        {
            return false;
        }
        return time >= hours.Open && time < hours.Close;
    }
}