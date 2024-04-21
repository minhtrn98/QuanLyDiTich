using Microsoft.Extensions.Caching.Memory;
using QLDT.Application.Common.Services;

namespace QLDT.Infrastructure.Common.Caching;

public sealed record MemoryCacheService(IMemoryCache MemoryCache) : IMemoryCacheService
{
    public T? Get<T>(string key)
    {
        return MemoryCache.Get<T>(key);
    }

    public void Remove(string key)
    {
        MemoryCache.Remove(key);
    }

    public void RemoveWithKeyPrefix(string keyPrefix)
    {
        throw new NotImplementedException();
    }

    public void Set<T>(string key, T valueToCache, MemoryCacheEntryOptions options)
    {
        MemoryCache.Set(key, valueToCache, options);
    }
}
