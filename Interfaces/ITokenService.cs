using api.Models;

namespace api.Interfaces;

public abstract class ITokenService
{
    public abstract string CreateToken(AppUser user);
}