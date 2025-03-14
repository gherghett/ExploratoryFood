using System;
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
        string extraInstructions,
        CustomerInfo customerInfo, 
        string deliveryInstructions)
    {
        var item = await _itemRepository.GetByIdAsync(menuItemId);

        if(item is null)
            Console.WriteLine("this is bad");

        var orderInfo = new OrderInfo {
            MenuItemId = menuItemId,
            MenuItemName = item.Name,
            Price = item.Price,
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
