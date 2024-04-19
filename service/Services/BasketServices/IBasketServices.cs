using Repos.BasketRepository.Models;
using service.Services.BasketServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service.Services.BasketServices
{
    public interface IBasketServices
    {
        Task<CustomerBasketDto> GetBasketAysnc(string basketid);
        Task<CustomerBasketDto> updateBasketAsync(CustomerBasketDto basket);
        Task<bool> DeleteBasketAsync(string basketid);

    }
}
