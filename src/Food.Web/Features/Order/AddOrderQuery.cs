using System;
using Food.Web.Models;
using MediatR;

namespace Food.Web.Features.Order;

public class AddOrderQuery(NewOrder newOrder) : IRequest<int>
{
    public NewOrder NewOrder {get; set;} = newOrder;
}
