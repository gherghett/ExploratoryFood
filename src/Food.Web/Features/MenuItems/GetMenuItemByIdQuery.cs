using System;
using MediatR;
using Food.Web.Models;

namespace Food.Web.Features.MenuItems;

public class GetMenuItemByIdQuery(int id) : IRequest<MenuItemViewModel>
{
    public int MenuItemId {get; set;} = id;
}