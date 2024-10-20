using api.Models;

namespace api.interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
