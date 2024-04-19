using dataa.Entities.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.OrderService.Dtos
{
    public class OrderDto
    {

        public string BasketId { get; set; }

        public string BuyerEmail{ get; set;}
        [Required]
        public int DeliveryMethodId { get; set; }

        public AddressDto ShippingAddrees { get; set; }
    }
}
