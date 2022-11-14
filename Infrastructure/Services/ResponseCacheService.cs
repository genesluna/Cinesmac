using System.Data.Common;
using System.Text.Json;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Infrastructure.Services;

public class ResponseCacheService : IResponseCacheService
{
  private readonly IDatabase _db;
  public ResponseCacheService(IConnectionMultiplexer redis)
  {
    _db = redis.GetDatabase();
  }

  public async Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
  {
    if (response == null) return;

    var options = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    var serializedResponse = JsonSerializer.Serialize(response, options);

    await _db.StringSetAsync(cacheKey, serializedResponse, timeToLive);
  }

  public async Task CacheResponseHeadersAsync(string cacheKey, object response, TimeSpan timeToLive)
  {
    await CacheResponseAsync("headers|" + cacheKey, response, timeToLive);
  }

  public async Task<string> GetCachedResponseAsync(string cacheKey)
  {
    var cachedResponse = await _db.StringGetAsync(cacheKey);

    if (cachedResponse.IsNullOrEmpty) return null;

    return cachedResponse;
  }

  public async Task<Dictionary<string, List<string>>> GetCachedResponseHeadersAsync(string cacheKey)
  {
    var serealizedHeaders = await GetCachedResponseAsync("headers|" + cacheKey);

    if (string.IsNullOrEmpty(serealizedHeaders)) return null;



    return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(serealizedHeaders);
  }

}
