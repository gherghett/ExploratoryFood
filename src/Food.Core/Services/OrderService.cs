using System;
using System.Numerics;
using Food.Core.Model;

namespace Food.Core.Services;


public class OrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<MenuItem> _itemRepository;
    private readonly IRepository<Restaurant> _restaurantRepository;
    private readonly IRepository<Runner> _runnerRepository;

    public OrderService(
        IRepository<Order> orderRepository,
        IRepository<MenuItem> itemRepository, 
        IRepository<Restaurant> restaurantRepository,
        IRepository<Runner> runnerRepository
        )
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _restaurantRepository = restaurantRepository;
        _runnerRepository = runnerRepository;
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

        var pricing = await CalculatePrice(menuItemId, quantity);
        if (pricing != expectedPricing)
            throw new OrderException($"Pricing was not the same as was displayed; expected {expectedPricing} but was {pricing}. Is API running?");

        if (item is null)
            throw new OrderException("Could not load MenuItem");

        var restaurant = await _restaurantRepository.GetByIdAsync(item.RestaurantId);
        if (restaurant is null)
            throw new OrderException("Could not load restaurant");
        if (!restaurant.IsOpenForOrders())
            return new Result<Order>("Cannot order from restaurant bc it is not open for orders");

        var orderInfo = new OrderInfo
        {
            MenuItemId = menuItemId,
            MenuItemName = item.Name,
            Price = pricing,
            ExtraInstructions = extraInstructions,
        };

        var order = new Order
        {
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
        var item = await _itemRepository.GetByIdAsync(menuItemId);
        decimal unitPrice = item.Price;
        var sum = unitPrice * quantity;
        var serviceFee = sum * 0.05m;
        var total = sum + serviceFee;
        return new Pricing(unit: unitPrice, sum: sum, serviceFee: serviceFee, total: total);
    }

    public async Task<OrderStatus?> ChangeStatus(int OrderId, OrderStatus newStatus)
    {
        var order = await _orderRepository.GetByIdAsync(OrderId);
        if ((int)order.Status >= (int)newStatus) //Can't reverse order status
            return null;
        order.Status = newStatus;
        await _orderRepository.UpdateAsync(order);
        return order.Status;
    }

    public async Task<Result<Order>> SetRunner(int id, int setRunner)
    {
        var runner = await _runnerRepository.GetByIdAsync(setRunner);
        if( runner is null )
            return new Result<Order>($"No runner with id {id} was found");
        runner.ActiveOrderId = id;
        await _runnerRepository.UpdateAsync(runner);

        var order = await _orderRepository.GetByIdAsync(id);
        if( order.Status != OrderStatus.Confirmed )
            return new Result<Order>($"Order is not available, its status is {order.Status}");
        order.Status = OrderStatus.CourierAccepted;

        await _orderRepository.UpdateAsync(order);
        return new Result<Order>(order);
    }
}
