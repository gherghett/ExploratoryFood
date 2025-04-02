using System;
using MediatR;
using Food.Web.Models;
using Food.Core.Model;


namespace Food.Web.Features.Restaurants;

public class GetRestaurantByMenuItemIdQueryHandler : IRequestHandler<GetRestaurantByMenuItemIdQuery, RestaurantDetailsViewModel>
{
    private readonly IRepository<Restaurant> _restaurantRepository;
    private readonly IRepository<MenuItem> _itemRepository;
    public GetRestaurantByMenuItemIdQueryHandler(
        IRepository<Restaurant> restaurantRepository,
        IRepository<MenuItem> itemRepository
    )
    {
        _restaurantRepository = restaurantRepository;
        _itemRepository = itemRepository;
    }

    async Task<RestaurantDetailsViewModel> IRequestHandler<GetRestaurantByMenuItemIdQuery, RestaurantDetailsViewModel>.Handle(GetRestaurantByMenuItemIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(request.MenuItemId);
        var restaurant = await _restaurantRepository.GetByIdAsync(item!.RestaurantId);
        return new RestaurantDetailsViewModel {
            Id = restaurant!.Id,
            Name = restaurant.Name,
            ContactInfo = restaurant.ContactInfo,
            ImageUrl = restaurant.ImageUrl,
        };
    }
}
