using System;

namespace Food.Core.Services;

// For the api will we need crud here? or just use the repo directly?
public class MenuItemService
{
    private readonly IRepository<MenuItem> _itemRepository;

    public MenuItemService(IRepository<MenuItem> itemRepository)
    {
        _itemRepository = itemRepository;
    }
}
