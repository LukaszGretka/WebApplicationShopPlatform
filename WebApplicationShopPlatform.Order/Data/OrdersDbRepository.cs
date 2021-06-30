using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.Data.Context;
using WebApplicationShopPlatform.Order.DTOs;
using WebApplicationShopPlatform.Shared.Models;

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

        public async Task<DatabaseActionResult<OrderDTO>> AddOrder(OrderDTO order)
        {
            try
            {
                await _orderDbContext.AddAsync(order);
                await _orderDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return new DatabaseActionResult<OrderDTO>(false, exception: ex);
            }

            return new DatabaseActionResult<OrderDTO>(true);
        }

        public async Task<OrderDTO> GetOrderById(int id)
        {
            return await _orderDbContext.Orders.FindAsync(id);
        }
    }
}
