using System;
using System.Linq;
using Example.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Example.Repositories
{
    public class CachedRepository<T> : Repository<T>,  ICachedRepository<T> where T : class
    {
        private readonly string _cacheKey;
        private readonly IMemoryCache _cache;
        
        public CachedRepository(DbContext context, IMemoryCache cache) : base(context)
        {
            _cache = cache;
            _cacheKey = typeof(T).Name;
        }

        public new IQueryable<T> GetAll()
        {
            if (!_cache.TryGetValue(_cacheKey, out IQueryable<T> cacheEntry))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(60));

                cacheEntry = base.GetAll();

                _cache.Set(_cacheKey, cacheEntry, cacheEntryOptions);
            }

            return cacheEntry; 
        }
    }
}