namespace Food.Api.Endpoints;

using System.Reflection.Metadata;
using Food.Core.Services;
using Food.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Food.Core;

public class GetOrders {

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) 
         => app.MapGet("/order/", Handle)
            .WithName("Get Orders")
            .WithOpenApi();
        public async Task<IResult> Handle
        (
            [FromServices] IRepository<Order> orderService
        )
        {
            var result = await orderService.ListAsync();
            return Results.Json<List<Order>>(result);
        }

    }
}