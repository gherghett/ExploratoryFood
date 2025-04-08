using System.Text.Json;
using Food.Core;
using Food.Core.Model;

namespace UnitTests;

public class OpenHoursTest
{
    private class SettableClock(SettableClock.TimeObject timeObject) : IClock
    {
        public DateTime Now => timeObject.Time;
        public DayOfWeek Today => timeObject.Time.DayOfWeek;
        public TimeOnly CurrentTime => TimeOnly.FromDateTime(timeObject.Time);
        public class TimeObject 
        {
            public DateTime Time {get; set;}
        }
    }
    private class TestClock(DateTime now) : IClock
    {
        public DateTime Now => now;
        public DayOfWeek Today => now.DayOfWeek;
        public TimeOnly CurrentTime => TimeOnly.FromDateTime(now);
    }


    [Fact]
    public void AreClosedWithoutEntries()
    {
        var hours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>(), new TestClock(DateTime.Now));
        Assert.False(hours.IsOpenForOrders());
    }

    [Fact]
    public void IsOpenWhenOpen()
    {
        var clock = new TestClock(new DateTime(2025, 3, 19, 10, 00, 00)); // 10am ona wednesday
        var hours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) }
        }, clock);

        Assert.True(hours.IsOpenForOrders());
    }

    [Fact]
    public void OpensAndCloses()
    {
        var timeObject = new SettableClock.TimeObject();
        var clock = new SettableClock(timeObject); // 10am ona wednesday
        var hours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) }
        }, clock);

        timeObject.Time = new DateTime(2025, 3, 19, 8, 59, 00); // Act
        Assert.False(hours.IsOpenForOrders()); // Assert
        timeObject.Time = new DateTime(2025, 3, 19, 9, 00, 00); //React
        Assert.True(hours.IsOpenForOrders()); // Reassert
        timeObject.Time = new DateTime(2025, 3, 19, 22, 00, 00); // Act again
        Assert.False(hours.IsOpenForOrders()); // Dessert
    }

    [Fact]
    public void IsClosedAfterUpdate()
    {
        // Arrange
        var clock = new TestClock(new DateTime(2025, 3, 19, 10, 00, 00)); // 10am ona wednesday
        var hours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) }
        }, clock);
        
        // Act
        hours = hours.UpdateHours(DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(11, 0), new TimeOnly(22, 0)));
        
        Assert.False(hours.IsOpenForOrders());
    }

    [Fact]
    public void Serializes()
    {
        // Arrange
        var hours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) }
        });

        var options = new JsonSerializerOptions
        {
            // Converters = { new TimeOnlyConverter() },
            WriteIndented = false
        };

        // Act
        var serializedActual = JsonSerializer.Serialize(hours, options);

        // Expected JSON string
        var serializedExpected = "{\"Wednesday\":{\"Open\":\"09:00:00\",\"Close\":\"22:00:00\"}}";

        // Assert
        Assert.Equal(serializedExpected, serializedActual);
    }

    [Fact]
    public void Deserializes()
    {
        // Arrange
        var s = "{\"Wednesday\":{\"Open\":\"09:00:00\",\"Close\":\"22:00:00\"}}";

        // Act
        var deserialized = JsonSerializer.Deserialize<OpenHours>(s);

        // // Expected JSON string
        // var serializedExpected = "{\"Hours\":{\"Wednesday\":{\"Open\":\"09:00:00\",\"Close\":\"22:00:00\"}}}";

        Assert.NotNull(deserialized);

        // Assert
        // Assert.Equal(deserialized.Hours[DayOfWeek.Wednesday], serializedActual);
    }

    [Fact]
    public void Deserialize_OpenHours_Should_Fail_Like_Api()
    {
        // Arrange: Simulating JSON request body from API
        string invalidJson = "{\"Hours\":{\"Wednesday\":{\"Open\":\"09:00:00\",\"Close\":\"22:00:00\"}}}";

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Act & Assert: Expecting deserialization to fail just like in the API
        var exception = Assert.Throws<System.Text.Json.JsonException>(() =>
        {
            JsonSerializer.Deserialize<OpenHours>(invalidJson, options);
        });

        Assert.Contains("Invalid key", exception.Message);
    }
}
