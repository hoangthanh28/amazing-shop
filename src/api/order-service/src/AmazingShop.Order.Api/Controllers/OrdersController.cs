using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using AmazingShop.Order.Application.Order.Command;

namespace AmazingShop.Order.Controller
{
    [ApiController]
    [Route("odr/[controller]")]
    [Authorize(AuthenticationSchemes = "oidc")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<OrdersController> _logger;
        public OrdersController(IMediator mediator, ILogger<OrdersController> logger)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrderAsync()
        {
            var command = new GetAllOrder();
            var products = await _mediator.Send(command);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrderAsync(PlaceOrder command)
        {
            var order = await _mediator.Send(command);
            return Created($"/odr/orders/{order.Id}", null);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderByIdAsync(int id)
        {
            var command = new GetOrderById(id);
            var product = await _mediator.Send(command);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NoContent();
            }
        }
    }
}
