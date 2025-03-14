
using System;
using MediatR;
using Food.Web.Models;
using Food.Core;
using Food.Core.Model;


namespace Food.Web.Features.MenuItems;

public class GetMenuItemsForRestaurantQueryHandler : IRequestHandler<GetMenuItemsForRestaurantQuery, List<MenuItemViewModel>>
{
    private readonly IRepository<MenuItem> _itemRepository;
    public GetMenuItemsForRestaurantQueryHandler(IRepository<MenuItem> itemRepository)
    {
        _itemRepository = itemRepository;
    }
    public async Task<List<MenuItemViewModel>> Handle(GetMenuItemsForRestaurantQuery request, CancellationToken cancellationToken)
    {
        var items = await _itemRepository.ListAsync( new AllMenuItemsForRestaurantId(request.RestaurantId));
    
        var views = items.Select(m => new MenuItemViewModel{
            Id = m.Id,
            Name = m.Name,
            Price = m.Price,
            ImageUrl = m.ImageUrl
        }).ToList();
        
        return views;
    }
}
