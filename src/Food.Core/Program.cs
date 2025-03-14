using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Food.Core;
using Food.Core.Model;

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
        await context.Database.EnsureDeletedAsync(); // Deletes the database (for testing)
        await context.Database.EnsureCreatedAsync(); // Creates the database

        var restaurantRepository = new Repository<Restaurant>(context);
        var itemRepository = new Repository<MenuItem>(context);


        Console.WriteLine("Database setup complete. Adding restaurant...");

        // Create OpenHours
        var openHours = new OpenHours(new Dictionary<DayOfWeek, (TimeOnly, TimeOnly)>
        {
            { DayOfWeek.Monday, (new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Tuesday, (new TimeOnly(9, 0), new TimeOnly(22, 0)) },
            { DayOfWeek.Wednesday, (new TimeOnly(9, 0), new TimeOnly(22, 0)) }
        });

        // Create a new Restaurant
        var restaurant = new Restaurant
        {
            Name = "Pizza Palace",
            Address = "123 Food Street",
            ContactInfo = "555-1234",
            OpenHours = openHours,
            ImageUrl = "https://www.themealdb.com/images/media/meals/llcbn01574260722.jpg/medium"
        };


        // Save the restaurant via repository
        await restaurantRepository.AddAsync(restaurant);

        // Add a MenuItem using the Aggregate Root method
        // restaurant.AddMenuItem("Margherita Pizza", 10.99m, "https://example.com/pizza.jpg");
        var menuItem = new MenuItem {
            Name = "Margherita Pizza",
            Price = 100.0m,
            ImageUrl = "https://example.com/pizza.jpg",
            RestaurantId = restaurant.Id,
        };
        await itemRepository.AddAsync(menuItem);

        Console.WriteLine("Restaurant added successfully!");

        // Retrieve & Display Restaurant Info using Specification
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
