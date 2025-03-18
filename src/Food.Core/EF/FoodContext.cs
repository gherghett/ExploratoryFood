using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text.Json;

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
public class OpenHours
{
    private Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)> _hours;
    public IReadOnlyDictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)> Hours => _hours;

    // EF Core requires a parameterless constructor
    private OpenHours() => _hours = new Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)>();

    public OpenHours(Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)> hours)
    {
        _hours = new Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)>(hours);
    }

    // Method to update hours while maintaining immutability
    public OpenHours UpdateHours(DayOfWeek day, TimeOnly open, TimeOnly close)
    {
        var updatedHours = new Dictionary<DayOfWeek, (TimeOnly, TimeOnly)>(_hours)
        {
            [day] = (open, close)
        };

        return new OpenHours(updatedHours);
    }

    // Convert to JSON for database storage
    public string ToJson() => JsonSerializer.Serialize(_hours);

    // Restore from JSON
    public static OpenHours FromJson(string json)
    {
        var dict = JsonSerializer.Deserialize<Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)>>(json) 
                   ?? new Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)>();
        return new OpenHours(dict);
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
    public OpenHours OpenHours { get; set; } = null!;
}

// MenuItem entity  Aggregate Root
public class MenuItem : BaseEntity, IAggregate
{
    public string Name { get; set; } = null!;
    public int RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
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
    public int MenuItemId { get; set; }
    public string MenuItemName { get; set; } = null!;
    // public int RestaurantId {get; set;}
    public Pricing Price {get; set;} = null!;
    public string ExtraInstructions { get; set; } = null!;
}

// Order Aggregate Root
public class Order : BaseEntity, IAggregate
{
    public DateTime CreationDate {get; set;} = DateTime.Now;
    public CustomerInfo CustomerInfo { get; set; } = null!;
    public OrderInfo OrderDetails { get; set; } = null!;
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
            .HasOne(m => m.Restaurant)
            .WithMany() //We dont need the nav property
            .HasForeignKey(m => m.RestaurantId);


        modelBuilder.Entity<Restaurant>()
            .OwnsOne(r => r.OpenHours, builder =>
            {
                builder.Property(o => o.Hours)
                    .HasConversion(
                        v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = false }), 
                        v => JsonSerializer.Deserialize<Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)>>(
                            v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) 
                            ?? new Dictionary<DayOfWeek, (TimeOnly Open, TimeOnly Close)>()
                    );
            });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //remove comment out when migrating, or when using project prom outside
        // optionsBuilder.UseSqlite("Data Source=localdb.db");

        base.OnConfiguring(optionsBuilder);
    }

    // For design time operations
    public FoodDeliveryContext() { }

    public FoodDeliveryContext(DbContextOptions<FoodDeliveryContext> options) : base(options)
    {
    }
}