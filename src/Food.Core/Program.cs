using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Food.Core;
using Food.Core.Model;

using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("UnitTests")]

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Starting database setup...");

        using var context = new FoodDeliveryContext(
            new DbContextOptionsBuilder<FoodDeliveryContext>()
                .UseSqlite("Data Source=../Food.Core/localdb.db")
                .Options
            );
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        var restaurantRepository = new Repository<Restaurant>(context);
        var itemRepository = new Repository<MenuItem>(context);

        Console.WriteLine("Database setup complete. Adding restaurants...");

        var openHours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Monday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Tuesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) }
        });

        // 🍕 Pizza Palace
        var pizzaPalace = new Restaurant
        {
            Name = "Pizza Palace",
            Address = "123 Food Street",
            ContactInfo = "555-1234",
            OpenHours = openHours,
            ImageUrl = "https://www.themealdb.com/images/media/meals/llcbn01574260722.jpg/medium"
        };
        await restaurantRepository.AddAsync(pizzaPalace);

        await itemRepository.AddAsync(new MenuItem { Name = "Margherita Pizza", Price = 100.0m, ImageUrl = "https://example.com/pizza.jpg", RestaurantId = pizzaPalace.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Pepperoni Classic", Price = 120.0m, ImageUrl = "https://example.com/pizza2.jpg", RestaurantId = pizzaPalace.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Truffle Mushroom", Price = 135.0m, ImageUrl = "https://example.com/pizza3.jpg", RestaurantId = pizzaPalace.Id });

        // 🇮🇳 Spice n Rice
        var spiceOpenHours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Monday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Tuesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Thursday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) }
        });
        var spiceNRice = new Restaurant
        {
            Name = "Spice n Rice",
            Address = "Västerlånggatan 5, Borås",
            ContactInfo = "info@spicenrice.se",
            OpenHours = spiceOpenHours,
            ImageUrl = "https://www.themealdb.com/images/media/meals/llcbn01574260722.jpg/medium"
        };
        await restaurantRepository.AddAsync(spiceNRice);

        await itemRepository.AddAsync(new MenuItem { Name = "Butter Chicken", Price = 115.0m, ImageUrl = "https://example.com/butterchicken.jpg", RestaurantId = spiceNRice.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Paneer Tikka", Price = 95.0m, ImageUrl = "https://example.com/paneertikka.jpg", RestaurantId = spiceNRice.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Lamb Vindaloo", Price = 125.0m, ImageUrl = "https://example.com/lambvindaloo.jpg", RestaurantId = spiceNRice.Id });

        // 🍷 La Copita
        var copitaOpenHours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Monday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Tuesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Thursday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Friday, new OpenHourEntry(new TimeOnly(12, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Saturday, new OpenHourEntry(new TimeOnly(12, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Sunday, new OpenHourEntry(new TimeOnly(12, 0), new TimeOnly(22, 0)) }
        });
        var laCopita = new Restaurant
        {
            Name = "La Copita",
            Address = "Stora Brogatan 12, Borås",
            ContactInfo = "contact@lacopita.se",
            OpenHours = copitaOpenHours,
            ImageUrl = "https://www.themealdb.com/images/media/meals/llcbn01574260722.jpg/medium"
        };
        await restaurantRepository.AddAsync(laCopita);

        await itemRepository.AddAsync(new MenuItem { Name = "Duck à l'Orange", Price = 175.0m, ImageUrl = "https://example.com/duck.jpg", RestaurantId = laCopita.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Beef Wellington", Price = 190.0m, ImageUrl = "https://example.com/beefwellington.jpg", RestaurantId = laCopita.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Tiramisu", Price = 85.0m, ImageUrl = "https://example.com/tiramisu.jpg", RestaurantId = laCopita.Id });

        // 🍕 Funky Town
        var funkyOpenHours = new OpenHours(new Dictionary<DayOfWeek, OpenHourEntry>
        {
            { DayOfWeek.Monday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Tuesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Wednesday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Thursday, new OpenHourEntry(new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Friday, new OpenHourEntry(new TimeOnly(12, 0), new TimeOnly(22, 0)) }
        });
        var funkyTown = new Restaurant
        {
            Name = "Funky Town",
            Address = "Allégatan 33, Borås",
            ContactInfo = "funky@townpizza.se",
            OpenHours = funkyOpenHours,
            ImageUrl = "https://www.themealdb.com/images/media/meals/llcbn01574260722.jpg/medium"
        };
        await restaurantRepository.AddAsync(funkyTown);

        await itemRepository.AddAsync(new MenuItem { Name = "Funky Special", Price = 110.0m, ImageUrl = "https://example.com/funky.jpg", RestaurantId = funkyTown.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Kebab Pizza", Price = 105.0m, ImageUrl = "https://example.com/kebabpizza.jpg", RestaurantId = funkyTown.Id });
        await itemRepository.AddAsync(new MenuItem { Name = "Hawaii Twist", Price = 99.0m, ImageUrl = "https://example.com/hawaii.jpg", RestaurantId = funkyTown.Id });

        Console.WriteLine("All restaurants and menu items added successfully!");

        // Display
        var savedRestaurants = await restaurantRepository.ListAsync();

        foreach (var savedRestaurant in savedRestaurants)
        {
            Console.WriteLine($"\nRestaurant: {savedRestaurant.Name}, Address: {savedRestaurant.Address}");
            Console.WriteLine($"Contact: {savedRestaurant.ContactInfo}");
            Console.WriteLine("Open Hours:");
            foreach (var day in savedRestaurant.OpenHours.Hours!)
            {
                Console.WriteLine($"  {day.Key}: {day.Value.Open} - {day.Value.Close}");
            }

            var menuItems = await itemRepository.ListAsync(new AllMenuItemsForRestaurantId(savedRestaurant.Id));
            Console.WriteLine("\nMenu Items:");
            foreach (var item in menuItems)
            {
                Console.WriteLine($"  {item.Name} - ${item.Price} ({item.ImageUrl})");
            }
        }

    }
}
