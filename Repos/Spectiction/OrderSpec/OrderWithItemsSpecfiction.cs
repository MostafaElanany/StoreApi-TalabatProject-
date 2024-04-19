using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using dataa.Entities.OrderEntities;
using StackExchange.Redis;


namespace Repos.Spectiction.OrderSpec
{
    public class OrderWithItemsSpecfiction : BaseSpectiction<dataa.Entities.OrderEntities.Order>
    {
        public OrderWithItemsSpecfiction(string buyerEmail) : base(order=>order.BuyerEmail==buyerEmail)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            AddOrderByDescending(order => order.OrderDate);


        }
        public OrderWithItemsSpecfiction(Guid id , string buyerEmail) : base(order => order.BuyerEmail == buyerEmail&&order.Id==id)
        {
            AddInclude(order => order.OrderItems);
            AddInclude(order => order.DeliveryMethod);
            


        }
    }
}
