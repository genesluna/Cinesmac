namespace Application.Core;

public class PagingParameters
{
  private const int MaxPageSize = 50;
  public int Index { get; set; } = 1;
  private int _limit = 10;
  public int Limit
  {
    get => _limit;
    set => _limit = (value > MaxPageSize) ? MaxPageSize : value;
  }
}
