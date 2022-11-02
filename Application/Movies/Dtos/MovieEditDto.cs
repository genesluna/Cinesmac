namespace Application.Movies.Dtos;

public class MovieEditDto
{
  public Guid Id { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public string Director { get; set; }
  public string Genre { get; set; }
  public int Length { get; set; }
  public double IMDBScore { get; set; }
  public string ImageURL { get; set; }
  public bool Is3D { get; set; }
  public bool IsIMAX { get; set; }
}
