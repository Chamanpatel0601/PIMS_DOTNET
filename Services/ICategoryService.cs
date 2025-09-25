
using PIMS_DOTNET.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int categoryId);
        Task<CategoryDTO> CreateAsync(CategoryCreateDTO dto);
        Task<CategoryDTO?> UpdateAsync(CategoryUpdateDTO dto);
        Task<bool> DeleteAsync(int categoryId);
    }
}
