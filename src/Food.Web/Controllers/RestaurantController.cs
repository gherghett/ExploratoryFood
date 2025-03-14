using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Food.Web.Models;
using MediatR;
using Food.Web.Features.Restaurants.GetRestaurants;
using Food.Web.Features.MenuItems;

namespace Food.Web.Controllers;
public class RestaurantController : Controller
{
    private readonly IMediator _mediator;

    public RestaurantController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: RestaurantController
    public async Task<ActionResult> Index(int id)
    {
        var items = await _mediator.Send( new GetMenuItemsForRestaurantQuery(id));
        return View(items);
    }

}
