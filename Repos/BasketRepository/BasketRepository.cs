using Repos.BasketRepository.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Repos.BasketRepository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string basketid)
        {
            return await _database.KeyDeleteAsync(basketid);             
        }

        public async Task<CustomerBasket> GetBasketAysnc(string basketid)
        {
            var data = await _database.StringGetAsync(basketid);

          

            return data.IsNullOrEmpty ? null :JsonSerializer.Deserialize<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> updateBasketAsync(CustomerBasket basket)
        {
            var isCreated = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
            if(!isCreated) 
            {
                return null ;
            }
            return await GetBasketAysnc(basket.Id);
        }   
    }
}
