using GameProfile.Application;
using Microsoft.Extensions.Caching.Distributed;
using System.Collections.Concurrent;
using System.Text.Json;

namespace GameProfile.Persistence.Caching
{
    // почему интрефейс ссылается на удаленный файл
    public class CacheService : ICacheService
    {
        private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();

        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
        {
            string? cachedValue = await _distributedCache.GetStringAsync(key, cancellationToken);
            if(cachedValue is null)
            {
                return null;
            }
            // if not work use JsonConvert
            T? value = JsonSerializer.Deserialize<T>(cachedValue);
            return value;
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            await _distributedCache.RemoveAsync(key, cancellationToken);

            CacheKeys.TryRemove(key, out bool _);
        }

        public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken cancellationToken = default)
        {
           IEnumerable<Task> tasks = CacheKeys.Keys.Where(k=> k.StartsWith(prefixKey)).Select(k=> RemoveAsync(k,cancellationToken));

            await Task.WhenAll(tasks);
        }

        public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
        {
            string cacheValue = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key,cacheValue,cancellationToken);

            CacheKeys.TryAdd(key, false);
        }
    }
}
