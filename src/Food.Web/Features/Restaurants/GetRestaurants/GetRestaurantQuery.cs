using System;
using MediatR;
using Food.Web.Models;

namespace Food.Web.Features.Restaurants.GetRestaurants;

public class GetRestaurantsQuery : IRequest<List<RestaurantCardViewModel>>
{

}
