using System;
using System.ComponentModel.DataAnnotations;
using WebApplicationShopPlatform.Order.Enums;

namespace WebApplicationShopPlatform.Order.DTOs
{
    public class OrderDTO
    {
        [Key]
        public int ID { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime Date { get; set; }

        public Guid UserId { get; set; }

        public bool Paid { get; set; }

        public ShippingStatus ShippingStatus { get; set; }
    }
}
