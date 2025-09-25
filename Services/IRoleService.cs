using PIMS_DOTNET.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO?> GetByIdAsync(int roleId);
    }
}
