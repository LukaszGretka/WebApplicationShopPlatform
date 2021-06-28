using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.Data.Context;
using WebApplicationShopPlatform.Order.DTOs;

namespace WebApplicationShopPlatform.Order.Data
{
    public class OrdersDbRepository : IOrdersDbRepository
    {
        private readonly OrderDatabaseContext _orderDbContext;
        private readonly ILogger<OrdersDbRepository> _logger;

        public OrdersDbRepository(OrderDatabaseContext orderDbContext, ILogger<OrdersDbRepository> logger)
        {
            _orderDbContext = orderDbContext ?? throw new ArgumentNullException(nameof(orderDbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public OrderDTO AddOrder(OrderDTO order)
        {
            throw new NotImplementedException();
        }

        public OrderDTO GetOrderById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
