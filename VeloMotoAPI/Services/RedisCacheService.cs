using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using VeloMotoAPI.Services.Interfaces;

namespace VeloMotoAPI.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<RedisCacheService> _logger;
        private readonly DistributedCacheEntryOptions _defaultOptions;

        public RedisCacheService(IDistributedCache cache, ILogger<RedisCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
            _defaultOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
        }

        public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
        {
            var result = await GetAsync<T>(key);
            if (result != null)
            {
                _logger.LogInformation("Cache hit for key: {Key}", key);
                return result;
            }

            _logger.LogInformation("Cache miss for key: {Key}", key);
            result = await factory();
            await SetAsync(key, result, expiration);
            return result;
        }

        public async Task<T> GetAsync<T>(string key)
        {
            var value = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(value))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deserializing cached value for key: {Key}", key);
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? _defaultOptions.AbsoluteExpirationRelativeToNow
            };

            try
            {
                var serializedValue = JsonSerializer.Serialize(value);
                await _cache.SetStringAsync(key, serializedValue, options);
                _logger.LogInformation("Successfully cached value for key: {Key}", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error caching value for key: {Key}", key);
            }
        }

        public async Task RemoveAsync(string key)
        {
            try
            {
                await _cache.RemoveAsync(key);
                _logger.LogInformation("Successfully removed cache for key: {Key}", key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cache for key: {Key}", key);
            }
        }

        public async Task RemoveByPrefixAsync(string prefix)
        {
            // Note: This is a simplified implementation. In a real Redis environment,
            // you would use the SCAN command to find and delete keys by pattern
            _logger.LogWarning("RemoveByPrefixAsync is not fully implemented for Redis");
        }
    }
} 