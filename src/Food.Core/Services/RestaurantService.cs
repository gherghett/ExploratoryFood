using System;
using System.Numerics;
using Food.Core.Model;

namespace Food.Core.Services;

public class RestaurantService
{
    private readonly IRepository<Restaurant> _restaurantRepository;

    public RestaurantService(IRepository<Restaurant> restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<Result<Restaurant>> AddRestaurant(Restaurant restaurant)
    {
        try 
        {
            await _restaurantRepository.AddAsync(restaurant);
            var new_restaurant = await _restaurantRepository.GetByIdAsync(restaurant.Id);

            if( new_restaurant is not null)
                return new Result<Restaurant>(new_restaurant);
        } 
        catch (Exception ex)
        {
            return new Result<Restaurant>($"Could not add restaurant, {ex.Message}");
        }
        return new Result<Restaurant>("Could not add restaurant");

    }
}