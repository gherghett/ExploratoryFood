using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Food.Core.Model;
public class DayOfWeekJsonConverter : JsonConverter<DayOfWeek>
{
    public override DayOfWeek Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            if (Enum.TryParse(reader.GetString(), true, out DayOfWeek day))
            {
                return day;
            }
        }

        throw new JsonException($"Unable to convert value to {nameof(DayOfWeek)}.");
    }

    public override void Write(Utf8JsonWriter writer, DayOfWeek value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }

    // ✅ Override for dictionary keys
    public override DayOfWeek ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (Enum.TryParse(reader.GetString(), true, out DayOfWeek day))
        {
            return day;
        }
        throw new JsonException($"Invalid key '{reader.GetString()}' for {nameof(DayOfWeek)}.");
    }

    public override void WriteAsPropertyName(Utf8JsonWriter writer, DayOfWeek value, JsonSerializerOptions options)
    {
        writer.WritePropertyName(value.ToString()); // ✅ Store as a string key
    }
}
