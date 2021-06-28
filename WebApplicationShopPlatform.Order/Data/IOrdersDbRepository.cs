using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.DTOs;

namespace WebApplicationShopPlatform.Order.Data
{
    public interface IOrdersDbRepository
    {
        OrderDTO GetOrderById(int Id);
        
        OrderDTO AddOrder(OrderDTO order);
    }
}
