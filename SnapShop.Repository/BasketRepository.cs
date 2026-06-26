using SnapShop.Core.Models;
using SnapShop.Core.Repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SnapShop.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database; 
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database= redis.GetDatabase();

        }

        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);

        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var basket = await _database.StringGetAsync(id);
            return  basket.IsNull ? null : JsonSerializer.Deserialize<CustomerBasket>(basket) ;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var basketSerialized = JsonSerializer.Serialize(basket);
            var createdOrUpdated = await _database.StringSetAsync(basket.Id, basketSerialized, TimeSpan.FromDays(1));
            if (!createdOrUpdated)
            {
                return null;
            }else
            {
                return await GetBasketAsync(basket.Id);
            }
    }
}}
