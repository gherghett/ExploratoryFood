using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Food.Core;

namespace Food.Core.Model;


// Base entity for common properties
public abstract class BaseEntity
{
    public int Id { get; set; }
}

// Marker interface for aggregates
public interface IAggregate { }

// OrderStatus Enum
public enum OrderStatus
{
    Pending,
    Accepted,
    Prepared,
    OnWay,
    Delivered,
    Canceled
}

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


public record Pricing(decimal unit, decimal sum, decimal serviceFee, decimal total);

// Restaurant entity (Aggregate Root)
public class Restaurant : BaseEntity, IAggregate
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string ContactInfo { get; set; } = null!;
    public string ImageUrl {get; set;} = null!;

    [JsonConverter(typeof(OpenHoursJsonConverter))]
    public OpenHours OpenHours { get; set; } = null!;

    public bool IsOpenForOrders() =>
        OpenHours.IsOpenForOrders();
}

// MenuItem entity  Aggregate Root
public class MenuItem : BaseEntity, IAggregate
{
    public string Name { get; set; } = null!;
    public int RestaurantId { get; set; }

    // public Restaurant Restaurant { get; set; } = null!; //We dont need this
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
}

// Value Object for Customer Info
public class CustomerInfo
{
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}

// Value Object for Order Info
public class OrderInfo
{
    //TODO why is there no quantity here?
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; } = null!;
    // public int RestaurantId {get; set;}
    public Pricing Price {get; set;} = null!;
    public string ExtraInstructions { get; set; } = null!;
}

// Order Aggregate Root
public class Order : BaseEntity, IAggregate
{
    public int RestaurantId {get; set;}
    public DateTime CreationDate {get; set;} = DateTime.Now;
    public CustomerInfo CustomerInfo { get; set; } = null!;
    public OrderInfo OrderDetails { get; set; } = null!; // TODO Why 2 different names?
    public string DeliveryInstructions { get; set; } = null!;
    public OrderStatus Status { get; set;} = OrderStatus.Pending;

    public TimeSpan? TimeRemaining()
    {
        var delivery_time = CreationDate.AddHours(1);
        var remaining = delivery_time - DateTime.Now;
        if( remaining <= TimeSpan.Zero )
            return null;
        return remaining;
    }
}

// DbContext
public class FoodDeliveryContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().OwnsOne(o => o.CustomerInfo);
        modelBuilder.Entity<Order>().OwnsOne(o => o.OrderDetails, oi =>
        {
            oi.OwnsOne(oi => oi.Price); // Nested owned type
        });


        modelBuilder.Entity<MenuItem>()
            .HasOne<Restaurant>() // Shadow navigation only
            .WithMany()             // We dont need the nav property
            .HasForeignKey(m => m.RestaurantId);


        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.OpenHours, builder =>
            {
                builder.Property(o => o.Hours)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }), 
                        v => JsonSerializer.Deserialize<Dictionary<DayOfWeek, OpenHourEntry>>(
                            v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                            ?? new Dictionary<DayOfWeek, OpenHourEntry>()
                    );
            });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // this should be commented out except when migrating or running core program.cs
        //  when using project prom outside it should be commented out - ie removed
        // optionsBuilder.UseSqlite("Data Source=localdb.db");

        base.OnConfiguring(optionsBuilder);
    }

    // For design time operationsdotnet
    public FoodDeliveryContext() { }

    public FoodDeliveryContext(DbContextOptions<FoodDeliveryContext> options) : base(options)
    {
    }
}