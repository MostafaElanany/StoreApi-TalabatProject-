using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataa.Entities.OrderEntities
{
    public class Order:BaseEntity<Guid>
    {
        public enum OrderPaymentStatus
        {

            Pending,
            Received,
            Faild
        }

        public string  BuyerEmail {  get; set; }
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;

        public ShippingAddress ShippingAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }

        public int? DeliveryMethodId {  get; set; }

        public OrderPaymentStatus orderPaymentStatus { get; set; } = OrderPaymentStatus.Pending;

        public IReadOnlyList<OrderItem> OrderItems {  get; set; }

        public decimal SubTotal {  get; set; }

        public string? PaymentIntentId {  get; set; } 

        public decimal GetTotal()
            => SubTotal + DeliveryMethod.Price;


        public string? BasketId { get; set; }






    }
}
