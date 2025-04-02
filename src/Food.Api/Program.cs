using Food.Core.Services;
using Food.Core.Model;
using Food.Api.Endpoints;
using Food.Core;
using System.Text.Json;
using Swashbuckle.AspNetCore.SwaggerGen;


using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Swagger
builder.Services.AddEndpointsApiExplorer(); // Required for Minimal APIs
builder.Services.AddSwaggerGen(options =>
{
    // options.(); // Ensures it respects System.Text.Json settings
});


// Custom gus extention
builder.Services.AddEndpoints(typeof(Program).Assembly);

builder.Services.AddDbContext<FoodDeliveryContext>(options =>
    options.UseSqlite("Data Source=../Food.Core/localdb.db"));

// Service layer services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<RestaurantService>();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new OpenHoursJsonConverter());
    options.SerializerOptions.Converters.Add(new DayOfWeekJsonConverter());
    options.SerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
});

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

app.MapEndpoints(); //Part of Gus enpoint mapper found in extentions folder

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enables Swagger
    app.UseSwaggerUI(); // Enables Swagger UI
}

// Actually use the cors policy eralier defined
app.UseCors("AllowLocalhost");

app.UseHttpsRedirection();

app.Run();
