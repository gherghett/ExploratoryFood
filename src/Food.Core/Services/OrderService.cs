using System;
using System.Numerics;
using Food.Core.Model;

namespace Food.Core.Services;


public class OrderService
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<MenuItem> _itemRepository;


    public OrderService(IRepository<Order> orderRepository, IRepository<MenuItem> itemRepository)
    {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
    }

    public async Task<Order?> PlaceOrder(
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
            throw new OrderException($"Pricing was not the same as expected; expected {expectedPricing} but was {pricing}");

        if(item is null)
            Console.WriteLine("this is bad");

        var orderInfo = new OrderInfo {
            MenuItemId = menuItemId,
            MenuItemName = item.Name,
            Price = pricing,
            ExtraInstructions = extraInstructions,
        };

        var order = new Order {
            OrderDetails = orderInfo,
            CustomerInfo = customerInfo,
            DeliveryInstructions = deliveryInstructions
        };
        await _orderRepository.AddAsync(order);
        return order;
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

    // public class OrderInfo
    // {
    //     public int MenuItemId { get; set; }
    //     public string MenuItemName { get; set; } = null!;
    //     public decimal Price { get; set; }
    //     public string ExtraInstructions { get; set; } = null!;
    // }

    public async Task<OrderStatus?> ChangeStatus(int OrderId, OrderStatus newStatus)
    {
        var order = await _orderRepository.GetByIdAsync(OrderId);
        // if( order is null)
        //     return null;
        return null;
    }
}
