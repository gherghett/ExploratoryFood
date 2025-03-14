using System;
using MediatR;
using Food.Web.Models;
using Food.Core.Model;


namespace Food.Web.Features.Restaurants;

public class GetRestaurantByIdQueryHandler : IRequestHandler<GetRestaurantByIdQuery, RestaurantDetailsViewModel>
{
    private readonly IRepository<Restaurant> _restaurantRepository;
    public GetRestaurantByIdQueryHandler(IRepository<Restaurant> restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    async Task<RestaurantDetailsViewModel> IRequestHandler<GetRestaurantByIdQuery, RestaurantDetailsViewModel>.Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);
        return new RestaurantDetailsViewModel {
            Id = restaurant.Id,
            Name = restaurant.Name,
            ContactInfo = restaurant.ContactInfo,
            ImageUrl = restaurant.ImageUrl,
        };
    }
}
