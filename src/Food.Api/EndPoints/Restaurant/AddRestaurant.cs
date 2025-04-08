namespace Food.Api.Endpoints;

using System.Reflection.Metadata;
using Food.Core.Services;
using Food.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Food.Core;

public class AddRestaurant {

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) 
         => app.MapPost("/restaurants/create/", Handle)
            .WithName("AddRestaurant")
            .WithOpenApi();
            // .Produces<AddRestaurantResponse>(StatusCodes.Status201Created)
            // .Produces(StatusCodes.Status404NotFound);
        public async Task<IResult> Handle
        (
            [FromServices] RestaurantService restaurantService,
            [FromBody] AddRestaurantRequest request
        )
        {
            var result = await restaurantService.AddRestaurant(request.restaurant);
            if(!result.TryGet(out var restaurant))
            {
                return Results.NotFound();
            }
            return Results.Created($"/restaurant/{restaurant.Id}", restaurant);
        }

    }
    public record AddRestaurantRequest(Restaurant restaurant);
    public record AddRestaurantResponse(string uri, Restaurant Restaurant);
}