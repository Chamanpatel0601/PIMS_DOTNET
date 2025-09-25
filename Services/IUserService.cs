
using PIMS_DOTNET.DTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IUserService
    {
        Task<UserDTO?> RegisterAsync(UserRegisterDTO dto);
        Task<UserDTO?> AuthenticateAsync(UserLoginDTO dto);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO?> GetByIdAsync(Guid userId);
    }
}
