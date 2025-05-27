using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AwesomeShop.Services.Orders.Infrastructure.CacheStorage
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;

        public RedisCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var objectString = await _distributedCache.GetStringAsync(key);

            if (string.IsNullOrWhiteSpace(objectString))
            {
                Console.WriteLine("Cahce key not found");

                return default(T);
            }

            Console.WriteLine("Cahce found for key ", key);
            return JsonConvert.DeserializeObject<T>(objectString);
        }

        public async Task SetAsync<T>(string key, T data)
        {
            var memoryCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600),
                SlidingExpiration = TimeSpan.FromSeconds(1200),
            };

            var objectString = JsonConvert.SerializeObject(data);

            Console.WriteLine("Cahce set for key ", key);

            await _distributedCache.SetStringAsync(key, objectString, memoryCacheEntryOptions);
        }
    }
}