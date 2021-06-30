using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.Data;
using WebApplicationShopPlatform.Order.DTOs;
using WebApplicationShopPlatform.Order.Enums;
using WebApplicationShopPlatform.Order.Models.Results;
using WebApplicationShopPlatform.Order.Queries;
using WebApplicationShopPlatform.Order.Services.Abstract;
using WebApplicationShopPlatform.Shared.Models;

namespace WebApplicationShopPlatform.Order.Handlers
{
    public class ProcessOrderHandler : IRequestHandler<ProcessOrderQuery, ProcessOrderResult>
    {
        private readonly IOrdersDbRepository _ordersDbRepository;
        private readonly IOrderService _orderService;

        public ProcessOrderHandler(IOrdersDbRepository ordersDbRepository, IOrderService orderService)
        {
            _ordersDbRepository = ordersDbRepository ?? throw new ArgumentNullException(nameof(ordersDbRepository));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public async Task<ProcessOrderResult> Handle(ProcessOrderQuery request, CancellationToken cancellationToken)
        {
            DatabaseActionResult<OrderDTO> result = await _ordersDbRepository.AddOrder(new OrderDTO
            {
                ShippingStatus = ShippingStatus.New,
                Date = DateTime.Now,
                Paid = false,
                UserId = request.UserId,
                TotalPrice = _orderService.CalculateTotalPrice(request.Products)
            });

            return new ProcessOrderResult { IsSuccess = result.Success };
        }
    }
}
