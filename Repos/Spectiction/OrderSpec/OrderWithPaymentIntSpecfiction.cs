using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.Spectiction.OrderSpec
{
    public  class OrderWithPaymentIntSpecfiction : BaseSpectiction<dataa.Entities.OrderEntities.Order>
    {
        public OrderWithPaymentIntSpecfiction(string? paymentintentid) : base(order => order.PaymentIntentId == paymentintentid)
        {
         


        }


    }
}
