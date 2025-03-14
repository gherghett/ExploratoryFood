using System;
using MediatR;
using Food.Web.Models;
namespace Food.Web.Features.Restaurants;

public class GetRestaurantByIdQuery(int id) : IRequest<RestaurantDetailsViewModel>
{
    public int RestaurantId {get; set;} = id;
}
