using System.Collections.Generic;
using WebApplicationShopPlatform.Order.Models.Results;
using WebApplicationShopPlatform.Shared.Models;
using WebApplicationShopPlatform.Order.Models;

namespace WebApplicationShopPlatform.Order.Services.Abstract
{
    public interface IOrderService
    {
        // 
        // ProcessOrder() - save order at db, send confirmation email and payment request
        // CancelOrder() - order may be cannceled, need to impelemnt logic for that

        public ProcessOrderResult ProcessOrder();

        public decimal CalculateTotalPrice(IList<Product> products);

        public float CalculateTotalWeigth(IList<Product> products);
    }
}
