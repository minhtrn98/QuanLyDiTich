using Microsoft.Extensions.Caching.Memory;

namespace QLDT.Application.Common.Services;

public interface IMemoryCacheService
{
    T? Get<T>(string key);

    void Set<T>(string key, T valueToCache, MemoryCacheEntryOptions options);

    void Set<T>(string key, T valueToCache, int expirationTimeInSeconds = 60)
    {
        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(expirationTimeInSeconds)
        };

        Set(key, valueToCache, options);
    }

    void Set<T>(string key, T valueToCache, TimeSpan expirationTime)
    {
        MemoryCacheEntryOptions options = new()
        {
            AbsoluteExpirationRelativeToNow = expirationTime
        };

        Set(key, valueToCache, options);
    }

    void Remove(string key);

    void RemoveWithKeyPrefix(string keyPrefix);
}
