using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.Queries;

namespace WebApplicationShopPlatform.Order.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> ProcessOrder()
        {
            var result = await _mediator.Send(new ProcessOrderQuery());

            return Ok(result);
        }

        public IActionResult CancelOrder()
        {
            return Ok();
        }

        [HttpGet]
        public IActionResult GetOrderDetails()
        {
            return Ok();
        }
    }
}
