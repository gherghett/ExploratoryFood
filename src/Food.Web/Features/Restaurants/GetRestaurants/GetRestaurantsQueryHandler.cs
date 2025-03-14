using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Food.Web.Models;

namespace Food.Web.Features.Restaurants.GetRestaurants;

public class GetRestaurantsQueryHandler : IRequestHandler<GetRestaurantsQuery, List<RestaurantCardViewModel>>
{
    private readonly IRepository<Restaurant> _restaurantRepo;

    public GetRestaurantsQueryHandler(IRepository<Restaurant> restaurantRepo)
    {
        _restaurantRepo = restaurantRepo;
    }

    public async Task<List<RestaurantCardViewModel>> Handle(GetRestaurantsQuery request, CancellationToken cancellationToken)
    {
        var restaurants = await _restaurantRepo.ListAsync();
        
        var restaurantViewModels = restaurants.Select(r => new RestaurantCardViewModel
        {
            Name = r.Name,
            Id = r.Id,
            ImageUrl = r.ImageUrl
        }).ToList();

        return restaurantViewModels;
    }
}
