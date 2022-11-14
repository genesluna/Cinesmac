namespace Domain.Interfaces;

public interface IResponseCacheService
{
  Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
  Task CacheResponseHeadersAsync(string cacheKey, object response, TimeSpan timeToLive);
  Task<string> GetCachedResponseAsync(string cacheKey);
  Task<Dictionary<string, List<string>>> GetCachedResponseHeadersAsync(string cacheKey);
}
