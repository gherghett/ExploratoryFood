namespace Food.Api.Endpoints;

using System.Reflection.Metadata;
using Food.Core.Services;
using Food.Core.Model;
using Microsoft.AspNetCore.Mvc;

public class CalculatePrice {

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) 
         => app.MapPost("/calculate-price", Handle)
            .WithName("CalculatePrice")
            .WithOpenApi(); 
        public async Task<IResult> Handle
        (
            [FromServices] OrderService orderService,
            [FromBody] Request request
        )
        {
            var price = await orderService.CalculatePrice(request.menuItemId, request.quantity);
            return Results.Ok(new Response(price));
        }

    }
    public record Request(int menuItemId, int quantity);
    public record Response(Pricing Pricing);
}