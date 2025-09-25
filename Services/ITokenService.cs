using PIMS_DOTNET.Models;

namespace PIMS_DOTNET.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user, string roleName);
    }
}
