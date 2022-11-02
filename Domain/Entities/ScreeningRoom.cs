namespace Domain.Entities;

public class ScreeningRoom : BaseEntity
{
  public string Name { get; set; }
  public bool Is3DRoom { get; set; }
  public bool IsIMAXRoom { get; set; }
  public ICollection<Session> Sessions { get; set; }
}
