namespace Application.Users.Dtos;

public class UserDto
{
  public UserDto()
  {
  }

  public UserDto(string id, string name, string email, string token)
  {
    Id = id;
    Email = email;
    Name = name;
    Token = token;
  }

  public string Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Token { get; set; }
}
