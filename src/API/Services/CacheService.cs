using API.RickAndMorty.Interfaces.IServices;
using Microsoft.Extensions.Caching.Memory;

namespace API.RickAndMorty.Services
{
    public class CacheService : ICacheService
    {
        private readonly IServiceProvider _serviceProvider;

        public CacheService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

        }

        public void AddCache(string cacheKey, object value, int timeFromMinutes = 10)
        {
            var memooryCache = _serviceProvider.GetRequiredService<IMemoryCache>();

            MemoryCacheEntryOptions opt = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(timeFromMinutes),
            };

            memooryCache.Set(cacheKey, value);
        }

        public object? GetCacheByKey(string cacheKey)
        {

            var memooryCache = _serviceProvider.GetRequiredService<IMemoryCache>();

            memooryCache.TryGetValue(cacheKey, out object? value);
            if (value != null)
            {
                return value;
            }

            return null;
        }
    }
}