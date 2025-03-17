namespace Food.Api.Endpoints;

using System.Reflection.Metadata;
using Food.Core.Services;
using Food.Core.Model;

public class CalculatePriceEndpoint {
    private readonly OrderService _orderService;
    public CalculatePriceEndpoint(OrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<Response> Handle(Request request)
    {
        var price = await _orderService.CalculatePrice(request.menuItemId, request.quantity);
        return new Response(price);
    }
    public record Request(int menuItemId, int quantity);
    public record Response(Pricing Pricing);
}