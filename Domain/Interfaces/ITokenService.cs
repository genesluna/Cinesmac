using Domain.Entities.Identity;

namespace Domain.Interfaces;

public interface ITokenService
{
  string CreateToken(User user);
}
