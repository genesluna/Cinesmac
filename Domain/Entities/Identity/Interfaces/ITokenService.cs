namespace Domain.Entities.Identity.Interfaces;

public interface ITokenService
{
  string CreateToken(User user);
}
