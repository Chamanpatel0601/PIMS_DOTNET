using PIMS_DOTNET.Models;

namespace PIMS_DOTNET.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(User user, string password, int roleId);
        Task<User?> GetByIdAsync(Guid userId);
    }
}
