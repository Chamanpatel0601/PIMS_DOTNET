using PIMS_DOTNET.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO?> GetByIdAsync(int roleId);
        Task<RoleDTO> CreateAsync(RoleDTO dto);
        Task<RoleDTO?> UpdateAsync(RoleDTO dto);
        Task<bool> DeleteAsync(int roleId);
    }
}
