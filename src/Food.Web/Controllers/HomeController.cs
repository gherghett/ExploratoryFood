using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Food.Web.Models;

namespace Food.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepository<Restaurant> _restaurantRepo;

    public HomeController(ILogger<HomeController> logger, IRepository<Restaurant> restaurantRepo)
    {
        _logger = logger;
        _restaurantRepo = restaurantRepo;
    }

    public async Task<IActionResult> Index()
    {
        var restaurants = await _restaurantRepo.ListAsync();
        var restaurantViewModels = restaurants.Select(r =>
            new RestaurantCardViewModel
            {
                Name = r.Name,
                Id = r.Id,
                ImageUrl = r.ImageUrl
            }
        );
        return View(restaurantViewModels.ToList());
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
