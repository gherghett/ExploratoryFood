using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Food.Core.Model;

public class FoodDeliveryContext : DbContext
{
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Runner> Runners {get; set;}

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
        
        modelBuilder.Entity<Runner>()
            .HasOne<Order>() //No navigation
            .WithMany()
            .HasForeignKey(r => r.ActiveOrderId);

        modelBuilder.Entity<Runner>().HasData(
            new Runner { Id = 1, ActiveOrderId = null },
            new Runner { Id = 2, ActiveOrderId = null },
            new Runner { Id = 3, ActiveOrderId = null } 
        );
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