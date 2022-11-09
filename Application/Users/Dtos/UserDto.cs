namespace Application.Users.Dtos;

public class UserDto
{
  public UserDto()
  {
  }

  public UserDto(string email, string name, string token)
  {
    Email = email;
    Name = name;
    Token = token;
  }

  public string Email { get; set; }
  public string Name { get; set; }
  public string Token { get; set; }
}
