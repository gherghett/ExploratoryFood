using System;
using Food.Web.Models;
using MediatR;

namespace Food.Web.Features.Order;

public class GetOrderDetailsQuery(int id) : IRequest<OrderDetailsViewModel>
{
    public int OrderId {get; set;} = id;
}
