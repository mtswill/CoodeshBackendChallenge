using BackendChallenge.Core.Result;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace BackendChallenge.Core.Helpers
{
    public static class CacheHelper
    {
        private static IMemoryCache _memCache = new MemoryCache(new MemoryCacheOptions());

        public static void SetCache(string cacheKey, object input)
        {
            var entryOptions = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(1),
                AbsoluteExpiration = DateTime.UtcNow.AddHours(2)
            };

            _memCache.Set(cacheKey, JsonSerializer.Serialize(input), entryOptions);
        }

        public static string? GetCache(string cacheKey)
        {
            var cache = _memCache.Get(cacheKey);
            return cache is not null ? cache.ToString() : null;
        }

        public static bool TryGetCache<T>(string cacheKey, out Result<T> content)
        {
            var cached = GetCache(cacheKey);

            if (cached is null)
            {
                content = default!;
                return false;
            }

            content = JsonSerializer.Deserialize<Result<T>>(cached)!;
            return true;
        }
    }
}
