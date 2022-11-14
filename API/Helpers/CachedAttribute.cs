using System.Text;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers;

public class CachedAttribute : Attribute, IAsyncActionFilter
{
  private readonly int _timeToLiveSeconds;
  public CachedAttribute(int timeToLiveSeconds)
  {
    _timeToLiveSeconds = timeToLiveSeconds;
  }

  public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

    var cashKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
    var cachedResponse = await cacheService.GetCachedResponseAsync(cashKey);

    if (!string.IsNullOrEmpty(cachedResponse))
    {
      var contentResult = new ContentResult
      {
        Content = cachedResponse,
        ContentType = "application/json",
        StatusCode = 200
      };

      context.Result = contentResult;

      return;
    }

    var executedContext = await next();

    if (executedContext.Result is OkObjectResult okObjectResult)
    {
      await cacheService.CacheResponseAsync(cashKey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
    }
  }

  private string GenerateCacheKeyFromRequest(HttpRequest request)
  {
    var keyBuilder = new StringBuilder();

    keyBuilder.Append($"{request.Path}");

    foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
    {
      keyBuilder.Append($"|{key}-{value}");
    }

    return keyBuilder.ToString();
  }
}
