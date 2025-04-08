namespace Food.Api.Endpoints;

using System.Reflection.Metadata;
using Food.Core.Services;
using Food.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Food.Core;

public class UpdateOrder {

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) 
         => app.MapPost("/orders/update/{id:int}/", Handle)
            .WithName("UpdateOrder")
            .WithOpenApi();
        public async Task<IResult> Handle
        (
            [FromRoute] int id,
            [FromServices] IRepository<Order> orderRepository,
            [FromServices] OrderService orderService,
            [FromQuery] OrderStatus? setStatus,
            [FromQuery] int? setRunner
        )
        {
            var order = await orderRepository.GetByIdAsync(id);

            if(order is null)
            {
                return Results.NotFound<Order>(order);
            }

            if( setStatus is not null)
            {
                var orderWithNewStatus = await orderService.ChangeStatus(id, (OrderStatus)setStatus);
                if (orderWithNewStatus is null)
                {
                    return Results.Problem("Could not change status");
                }
            }

            if( setRunner is not null)
            {
                var result = await orderService.SetRunner(id, (int)setRunner);
                if (!result.IsSuccess)
                {
                    return Results.Problem(result.FailMessage);
                }
            }

            var updatedOrder = await orderRepository.GetByIdAsync(id);
            return Results.Accepted($"/orders/{id}", updatedOrder);
        }

    }
}