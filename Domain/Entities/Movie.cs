namespace Domain.Entities;

public class Movie : BaseEntity
{
  public string Title { get; set; }
  public string Description { get; set; }
  public string Director { get; set; }
  public string Genre { get; set; }
  public int Length { get; set; }
  public double IMDBScore { get; set; }
  public string ImageURL { get; set; }
  public bool Is3D { get; set; }
  public bool IsIMAX { get; set; }
  public ICollection<Session> Sessions { get; set; }
}
