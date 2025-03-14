using System;
using MediatR;
using Food.Web.Models;

namespace Food.Web.Features.Restaurants;

public class GetRestaurantsQuery : IRequest<List<RestaurantCardViewModel>>
{

}
