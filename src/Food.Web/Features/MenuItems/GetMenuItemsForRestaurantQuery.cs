using System;
using MediatR;
using Food.Web.Models;

namespace Food.Web.Features.MenuItems;

public class GetMenuItemsForRestaurantQuery(int id) : IRequest<List<MenuItemViewModel>>
{
    public int RestaurantId {get; set;} = id;
}
