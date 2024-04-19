using dataa.Entities;
using service.Services.OrderService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.OrderService
{
    public interface IOrderService
    {

        Task<OrderResultDto> CreateOrderAsync(OrderDto input);
        Task<IReadOnlyList<OrderResultDto>> GetAllOrderUserAsync(string BuyerEmail);
        Task<OrderResultDto> GetOrderByIdAsync(Guid id,string BBuyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethodsAsync();




    }

}
