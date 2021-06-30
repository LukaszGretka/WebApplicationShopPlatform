using MediatR;
using System;
using System.Collections.Generic;
using WebApplicationShopPlatform.Order.Models.Results;
using WebApplicationShopPlatform.Shared.Models;

namespace WebApplicationShopPlatform.Order.Queries
{
    public class ProcessOrderQuery : IRequest<ProcessOrderResult>
    {
        public IList<Product> Products { get; set; }

        public Guid UserId { get; set; }
    }
}
