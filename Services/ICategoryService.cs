using PIMS_DOTNET.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int categoryId);
        Task<Category> CreateAsync(Category category);
        Task<Category?> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int categoryId);
    }
}
