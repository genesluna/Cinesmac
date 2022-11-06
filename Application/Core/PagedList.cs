using Microsoft.EntityFrameworkCore;

namespace Application.Core;

public class PagedList<T> : List<T>
{
  public PagedList(IEnumerable<T> items, int count, int index, int limit)
  {
    TotalCount = count;
    Index = index;
    Limit = limit;
    PageCount = (int)Math.Ceiling(count / (double)limit);
    AddRange(items);
  }

  public int Index { get; set; }
  public int Limit { get; set; }
  public int PageCount { get; set; }
  public int TotalCount { get; set; }

  public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int index, int limit, CancellationToken cancellationToken)
  {
    var count = await source.CountAsync();
    var items = await source.Skip((index - 1) * limit).Take(limit).ToListAsync(cancellationToken);

    return new PagedList<T>(items, count, index, limit);
  }
}
