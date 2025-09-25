
using PIMS_DOTNET.DTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IProductCategoryService
    {
        Task<IEnumerable<ProductCategoryDTO>> GetAllAsync();
        Task<IEnumerable<ProductCategoryDTO>> GetByProductIdAsync(Guid productId);
        Task<IEnumerable<ProductCategoryDTO>> GetByCategoryIdAsync(int categoryId);
        Task<ProductCategoryDTO?> AddAsync(ProductCategoryCreateDTO dto);
        Task<bool> RemoveAsync(Guid productId, int categoryId);
    }
}
