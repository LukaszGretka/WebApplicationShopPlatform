using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.Data;
using WebApplicationShopPlatform.Order.DTOs;
using WebApplicationShopPlatform.Order.Models.Results;
using WebApplicationShopPlatform.Order.Queries;
using WebApplicationShopPlatform.Order.Services.Abstract;

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
            var result = new ProcessOrderResult();

            _ordersDbRepository.AddOrder(new OrderDTO
            {

                TotalPrice = _orderService.CalculateTotalPrice(request.Products)
            });



            return result;
        }
    }
}
