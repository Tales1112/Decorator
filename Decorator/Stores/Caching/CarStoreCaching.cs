using Decorator.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Decorator.Stores.Caching
{
    public class CarStoreCaching : ICarStore
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ICarStore _inner;
        private readonly ILogger<CarStoreCaching> _logger;

        public CarStoreCaching(ICarStore inner, IMemoryCache memoryCache, ILogger<CarStoreCaching> logger)
        {
            _memoryCache = memoryCache;
            _inner = inner;
            _logger = logger;
        }

        public CarDto Get(int id)
        {
            var key = "Cars";
            var items = _memoryCache.Get<CarDto>(key);
            if (items == null)
            {
                items = _inner.Get(id);
                _logger.LogTrace("Cache miss for {CacheKey}", key);
                _memoryCache.Set(key, items, System.TimeSpan.FromMinutes(1));
            }
            else
            {
                items.FromMemory();
                _logger.LogTrace("Cache hit for {CacheKey}", key);
            }
            return items;
        }

        public CarDto List()
        {
            var key = "Cars";
            var items = _memoryCache.Get<CarDto>(key);
            if (items == null)
            {
                items = _inner.List();
                _logger.LogTrace("Cache miss for {CacheKey}", key);
                _memoryCache.Set(key, items, System.TimeSpan.FromMinutes(1));
            }
            else
            {
                items.FromMemory();
                _logger.LogTrace("Cache hit for {CacheKey}", key);
            }
            return items;
        }
    }
}
