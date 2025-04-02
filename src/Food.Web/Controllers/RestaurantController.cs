using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Food.Web.Models;
using MediatR;
using Food.Web.Features.Restaurants;
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

    public async Task<ActionResult> Order(int id)
    {
        var item  = await _mediator.Send(new GetMenuItemByIdQuery(id));
        ViewData["MenuItem"]  = await _mediator.Send(new GetMenuItemByIdQuery(id));
        ViewData["Restaurant"] = await _mediator.Send(new GetRestaurantByMenuItemIdQuery(id));
        return View();
    }

}
