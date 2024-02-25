using CompanyPortal.Core.Interfaces;

using Microsoft.Extensions.Caching.Memory;

namespace CompanyPortal.Services;

public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(5);

    public async Task<T?> GetOrCreateAsync<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expiration = null, CancellationToken cancellation = default)
    {
        var result = await memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.SetAbsoluteExpiration(expiration ?? DefaultExpiration);
            return factory(cancellation);
        });
        return result;
    }
}
