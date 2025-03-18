using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Food.Web.Models;
using MediatR;
using Food.Web.Features.Restaurants;
using Food.Web.Features.Order;
using Food.Core.Model;


namespace Food.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrderController> _logger;
        public OrderController(ILogger<OrderController> logger, IMediator mediator )
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> New(NewOrder newOrder)
        {
            int orderId = await _mediator.Send(new AddOrderQuery(newOrder));
            
            if (orderId != 0)
                return RedirectToAction("Watch", new { id = orderId });

            return BadRequest("Order creation failed. Please try again.");
        }

        public async Task<ActionResult> Watch(int id)
        {
            var orderDetails = await _mediator.Send(new GetOrderDetailsQuery(id));

            return View(orderDetails);
        }

    }
}
