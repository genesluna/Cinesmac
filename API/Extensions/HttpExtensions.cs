namespace API.Extensions;

public static class HttpExtensions
{
  public static void AddPaginationsHeaders(this HttpResponse response, int index,
        int limit, int pageCount, int totalCount)
  {
    response.Headers.Add("Pagination-Index", index.ToString());
    response.Headers.Add("Pagination-Items-Count", totalCount.ToString());
    response.Headers.Add("Pagination-Limit", limit.ToString());
    response.Headers.Add("Pagination-Pages-Count", pageCount.ToString());
    response.Headers.Add("Access-Control-Expose-Headers", "*");
  }
}
