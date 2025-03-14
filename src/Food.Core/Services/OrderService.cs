using System;

namespace Food.Core.Services;

public class OrderService
{
    private readonly IRepository<Order> _orderRepository;

    public OrderService(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Order?> PlaceOrder(
        OrderInfo orderInfo, 
        CustomerInfo customerInfo, 
        string deliveryInstructions)
    {
        var order = new Order {
            OrderDetails = orderInfo,
            CustomerInfo = customerInfo,
            DeliveryInstructions = deliveryInstructions
        };
        await _orderRepository.AddAsync(order);
        return order;
    }
}
