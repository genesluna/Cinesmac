namespace Application.ScreeningRooms.Dtos;

public class ScreeningRoomDto
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public bool Is3DRoom { get; set; }
  public bool IsIMAXRoom { get; set; }
}
