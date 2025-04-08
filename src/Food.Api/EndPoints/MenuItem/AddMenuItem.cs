namespace Food.Api.Endpoints;

using System.Reflection.Metadata;
using Food.Core.Services;
using Food.Core.Model;
using Microsoft.AspNetCore.Mvc;
using Food.Core;

public class AddMenuItem {

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app) 
         => app.MapPost("/menu-items/create/", Handle)
            .WithName("AddMenuItem")
            .WithOpenApi();
            // .Produces<AddRestaurantResponse>(StatusCodes.Status201Created)
            // .Produces(StatusCodes.Status404NotFound);
        public async Task<IResult> Handle
        (
            [FromServices] IRepository<MenuItem> itemRepository,
            [FromBody] AddMenuItemRequest request
        )
        {
            await itemRepository.AddAsync(request.menuItem);
            var item = await itemRepository.GetByIdAsync(request.menuItem.Id);
            if(item is null)
            {
                return Results.NotFound();
            }
            return Results.Created($"/menu-item/{item.Id}", item); //TODO this address does not exist
        }

    }
    public record AddMenuItemRequest(MenuItem menuItem);
    public record AddMenuItemResponse(string uri, MenuItem menuItem);
}