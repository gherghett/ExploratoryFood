using System;
using MediatR;
using Food.Web.Models;
using Food.Core;
using Food.Core.Services;
using Food.Core.Model;



namespace Food.Web.Features.Order;

public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDetailsViewModel>
{
    private readonly IRepository<Food.Core.Model.Order> _orderRepository;
    private readonly IRepository<Food.Core.Model.MenuItem> _itemRepository;
    private readonly IRepository<Food.Core.Model.Restaurant> _restaurantRepository;

    public GetOrderDetailsQueryHandler(
        IRepository<Food.Core.Model.Order> orderRepository,
        IRepository<MenuItem> itemRepository,
        IRepository<Restaurant> restaurantRepository
        )
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _restaurantRepository = restaurantRepository;
    }
    public async Task<OrderDetailsViewModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        var item = await _itemRepository.GetByIdAsync(order!.OrderDetails.MenuItemId);
        var restaurant = await _restaurantRepository.GetByIdAsync(item!.RestaurantId);
        var orderDetails = OrderDetailsViewModel.FromOrder(order, restaurant!, item);
        return orderDetails;
    }
}
