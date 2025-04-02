using System;
using MediatR;
using Food.Web.Models;
namespace Food.Web.Features.Restaurants;

public class GetRestaurantByMenuItemIdQuery(int id) : IRequest<RestaurantDetailsViewModel>
{
    public int MenuItemId {get; set;} = id;
}
