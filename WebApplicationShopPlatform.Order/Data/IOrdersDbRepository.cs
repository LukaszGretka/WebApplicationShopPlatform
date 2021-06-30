using System.Threading.Tasks;
using WebApplicationShopPlatform.Order.DTOs;
using WebApplicationShopPlatform.Shared.Models;

namespace WebApplicationShopPlatform.Order.Data
{
    public interface IOrdersDbRepository
    {
        Task<OrderDTO> GetOrderById(int Id);

        Task<DatabaseActionResult<OrderDTO>> AddOrder(OrderDTO order);
    }
}
