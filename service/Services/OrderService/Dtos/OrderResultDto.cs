using dataa.Entities.OrderEntities;
using dataa.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static dataa.Entities.OrderEntities.Order;

namespace service.Services.OrderService.Dtos
{
    public class OrderResultDto
    {

        public Guid Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public AddressDto ShippingAddress { get; set; }

        public string DeliveryMethodName { get; set; }

        public OrderPaymentStatus orderPaymentStatus { get; set; } 

        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }

        public decimal SubTotal { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal Total { get; set; }
        public string? PaymentIntentId { get; set; }

        public string? BasketId { get; set; }





    }
}
