using System;
using System.Numerics;
using Food.Core.Model;

namespace Food.Core.Services;


public class OrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<MenuItem> _itemRepository;
    private readonly IRepository<Restaurant> _restaurantRepository;

    public OrderService(IRepository<Order> orderRepository, IRepository<MenuItem> itemRepository, IRepository<Restaurant> restaurantRepository)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _restaurantRepository = restaurantRepository;
    }

    public async Task<Result<Order>> PlaceOrder(
        int restaurantId,
        int menuItemId, 
        int quantity,
        string extraInstructions,
        CustomerInfo customerInfo, 
        string deliveryInstructions,
        Pricing expectedPricing
        )
    {
        var item = await _itemRepository.GetByIdAsync(menuItemId);

        var pricing = await CalculatePrice(menuItemId,quantity );
        if(pricing != expectedPricing)
            throw new OrderException($"Pricing was not the same as was displayed; expected {expectedPricing} but was {pricing}. Is API running?");

        if(item is null)
            throw new OrderException("Could not load MenuItem");

        var restaurant = await _restaurantRepository.GetByIdAsync(item.RestaurantId);
        if(restaurant is null)
            throw new OrderException("Could not load restaurant");
        if(!restaurant.IsOpenForOrders())
            return new Result<Order>("Cannot order from restaurant bc it is not open for orders");

        var orderInfo = new OrderInfo {
            MenuItemId = menuItemId,
            MenuItemName = item.Name,
            Price = pricing,
            ExtraInstructions = extraInstructions,
        };

        var order = new Order {
            RestaurantId = restaurantId,
            OrderDetails = orderInfo,
            CustomerInfo = customerInfo,
            DeliveryInstructions = deliveryInstructions
        };
        await _orderRepository.AddAsync(order);
        return new Result<Order>(order);
    }

    public async Task<Pricing> CalculatePrice(int menuItemId, int quantity)
    {
        var item =  await _itemRepository.GetByIdAsync(menuItemId);
        decimal unitPrice = item.Price;
        var sum = unitPrice * quantity;
        var serviceFee = sum * 0.05m;
        var total = sum + serviceFee;
        return new Pricing(unit: unitPrice, sum: sum, serviceFee: serviceFee, total: total);
    }

    public async Task<OrderStatus?> ChangeStatus(int OrderId, OrderStatus newStatus)
    {
        var order = await _orderRepository.GetByIdAsync(OrderId);
        return null;
    }
}
