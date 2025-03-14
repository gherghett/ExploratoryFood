using MediatR;
using Food.Web.Models;
using System;
using Food.Core.Model;

namespace Food.Web.Features.MenuItems;

public class GetMenuItemByIdQueryHandler : IRequestHandler<GetMenuItemByIdQuery, MenuItemViewModel>
{
    private readonly IRepository<MenuItem> _itemRepository;
    public GetMenuItemByIdQueryHandler(IRepository<MenuItem> itemRepository)
    {
        _itemRepository = itemRepository;
    }
    public async Task<MenuItemViewModel> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(request.MenuItemId);
        return new MenuItemViewModel {
            Id = item.Id,
            Name = item.Name,
            Price = item.Price,
            ImageUrl = item.ImageUrl,
        };
    }
}
