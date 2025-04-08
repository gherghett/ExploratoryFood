using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Food.Api;
using Food.Core.Model;

public class ApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var dbPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"../../../../../src/Food.Core/localdb.db"));
            Console.WriteLine(dbPath);
            if (!File.Exists(dbPath))
            {
                throw new FileNotFoundException($"SQLite database file not found at: {dbPath}");
            }

            services.RemoveAll<DbContextOptions<FoodDeliveryContext>>();

            services.AddDbContext<FoodDeliveryContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
            });

            // Ensure DB file and schema
            var sp = services.BuildServiceProvider();

            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<FoodDeliveryContext>();
        });
    }
}
