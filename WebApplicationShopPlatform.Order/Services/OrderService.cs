﻿using System;
using System.Collections.Generic;
using WebApplicationShopPlatform.Order.Models.Results;
using WebApplicationShopPlatform.Order.Services.Abstract;
using WebApplicationShopPlatform.Shared.Models;

namespace WebApplicationShopPlatform.Order.Services
{
    public class OrderService : IOrderService
    {
        public decimal CalculateTotalPrice(IList<ProductDTO> products)
        {
            throw new NotImplementedException();
        }

        public float CalculateTotalWeigth(IList<ProductDTO> products)
        {
            throw new NotImplementedException();
        }

        public ProcessOrderResult ProcessOrder()
        {
            throw new NotImplementedException();
        }
    }
}
