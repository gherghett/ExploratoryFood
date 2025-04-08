// namespace Food.Api.Endpoints;

// using Food.Core.Services;
// using Food.Core.Model;
// using Microsoft.AspNetCore.Mvc;
// using Food.Core;

// public class GetFilteredOrders
// {
//     public class Endpoint : IEndpoint
//     {
//         public void MapEndpoint(IEndpointRouteBuilder app) 
//          => app.MapGet("/orders", Handle)
//             .WithName("Get Filtered Orders")
//             .WithOpenApi();
            
//         public async Task<IResult> Handle
//         (
//             [FromServices] IRepository<Order> orderService,
//             [FromQuery] int? restaurantId,
//             [FromQuery] OrderStatus? status
//         )
//         {
//             var specification = new OrderFilterSpecification(restaurantId, status);
//             var result = await orderService.ListAsync(specification);
//             return Results.Json(result);
//         }
//     }
// }