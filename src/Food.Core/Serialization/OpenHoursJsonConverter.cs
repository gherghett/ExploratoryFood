using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using Food.Core.Model;
namespace Food.Core;

using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class OpenHoursJsonConverter : JsonConverter<OpenHours>
{
    public override OpenHours Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var customOptions = new JsonSerializerOptions(options);
        customOptions.Converters.Add(new DayOfWeekJsonConverter()); // ✅ Ensure DayOfWeek is handled correctly

        var dictionary = JsonSerializer.Deserialize<Dictionary<DayOfWeek, OpenHourEntry>>(ref reader, customOptions);
        return new OpenHours(dictionary ?? new Dictionary<DayOfWeek, OpenHourEntry>());
    }

    public override void Write(Utf8JsonWriter writer, OpenHours value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Hours, options);
    }
}