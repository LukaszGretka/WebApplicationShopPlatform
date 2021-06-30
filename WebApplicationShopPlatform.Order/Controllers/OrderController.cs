using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.Models;
using WebApplicationShopPlatform.Order.Models.Results;
using WebApplicationShopPlatform.Order.Queries;
using WebApplicationShopPlatform.Shared.Models;

namespace WebApplicationShopPlatform.Order.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        [Route("process")]
        public async Task<IActionResult> ProcessOrder(Guid userId, [FromBody] OrderProducts orderProducts)
        {
            ProcessOrderResult result = await _mediator.Send(new ProcessOrderQuery
            { 
                UserId = userId,
                Products = orderProducts.Products
            });
                
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
