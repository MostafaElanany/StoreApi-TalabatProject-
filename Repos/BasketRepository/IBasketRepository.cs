using Repos.BasketRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repos.BasketRepository
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAysnc(string basketid);
        Task<CustomerBasket> updateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string basketid);

    }
}
