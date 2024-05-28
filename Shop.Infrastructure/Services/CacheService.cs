using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Shop.Application.Common.Interfaces;

namespace Shop.Infrastructure.Services;

public class CacheService(IDistributedCache cache) : ICacheService
{
    public async Task<T?> GetAsync<T>(string key)
    {
        var stringData = await cache.GetStringAsync(key);
        if (stringData is not null)
        {
            JsonSerializerOptions options = new()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                WriteIndented = true,
            };

            return JsonSerializer.Deserialize<T>(stringData, options);
        }

        return default;
    }

    public async Task SetAsync<T>(T data, string key, TimeSpan? slidingExpiration = null)
    {
        JsonSerializerOptions options = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true,
        };

        var toCache = JsonSerializer.Serialize(data, options);
        var cacheOptions = new DistributedCacheEntryOptions();
        cacheOptions.SetAbsoluteExpiration(slidingExpiration ?? TimeSpan.FromMinutes(10));
        cacheOptions.SetSlidingExpiration(slidingExpiration ?? TimeSpan.FromMinutes(10));
        await cache.SetStringAsync(key, toCache, cacheOptions);
    }
}