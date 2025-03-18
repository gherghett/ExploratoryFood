using System;
using MediatR;
using Food.Core;
using Food.Core.Services;
using Food.Core.Model;


namespace Food.Web.Features.Order;

public class AddOrderQueryHandler : IRequestHandler<AddOrderQuery, int>
{
    private readonly OrderService _orderService;
    public AddOrderQueryHandler(OrderService orderService)
    {
        _orderService = orderService;
    }
    public async Task<int> Handle(AddOrderQuery request, CancellationToken cancellationToken)
    {
        var orderResult = await _orderService.PlaceOrder(
            request.NewOrder.MenuItemId, 
            request.NewOrder.Quantity,
            request.NewOrder.ExtraInstructions ?? "",
            new CustomerInfo {
                Name = request.NewOrder.Name,
                Address = request.NewOrder.Address,
                PhoneNumber = request.NewOrder.PhoneNumber,
            },
            request.NewOrder.DeliveryInstructions ?? "",
            request.NewOrder.ExpectedPricing);

        if(!orderResult.TryGet(out var order))
            return 0;
        
        return order.Id;
    }
}
