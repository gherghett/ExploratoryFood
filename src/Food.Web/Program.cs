using Microsoft.EntityFrameworkCore;
// Mediator========
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection; //For Assedotentmbly
//========mediator=


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<FoodDeliveryContext>(options =>
    options.UseSqlite("Data Source=../Food.Core/localdb.db"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

// Register MediatR and scan the assembly for handlers
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
