using Food.Core.Services;
using Food.Core.Model;
using Food.Api.Endpoints;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//App level services
builder.Services.AddEndpointsApiExplorer(); // Required for Minimal APIs
builder.Services.AddDbContext<FoodDeliveryContext>(options =>
    options.UseSqlite("Data Source=../Food.Core/localdb.db"));
builder.Services.AddSwaggerGen(); // Registers Swagger

//Service layer services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<OrderService>();

//Endpoints 
builder.Services.AddScoped<CalculatePriceEndpoint>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",

        policy =>
        {
            policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            // policy.WithOrigins("http://localhost:5200") 
            //       .AllowAnyHeader()
            //       .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger
    app.UseSwaggerUI(); // Enables Swagger UI
}

// Actually use the cors policy
app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi(); // Exposes endpoint in Swagger

app.MapPost("/calculate-price", async (CalculatePriceEndpoint endpoint, CalculatePriceEndpoint.Request request) =>
{
    return await endpoint.Handle(request);
})
.WithName("CalculatePrice")
.WithOpenApi(); //swagger

// policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();


app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
