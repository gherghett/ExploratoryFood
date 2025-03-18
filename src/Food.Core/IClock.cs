// A clock interface for testing

namespace Food.Core.Model;

public interface IClock
{
    DateTime Now { get; }
    DayOfWeek Today { get; }
    TimeOnly CurrentTime { get; }
}

// The standard one
public class SystemClock : IClock
{
    public DateTime Now => DateTime.Now;
    public DayOfWeek Today => DateTime.Now.DayOfWeek;
    public TimeOnly CurrentTime => TimeOnly.FromDateTime(DateTime.Now);
}

// Tests can implement the IClock and insert whatever times they want