
using PIMS_DOTNET.DTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByIdAsync(Guid productId);
        Task<ProductDTO> CreateAsync(ProductCreateDTO dto);
        Task<ProductDTO?> UpdateAsync(ProductUpdateDTO dto);
        Task<bool> DeleteAsync(Guid productId);

        // Business Logic
        Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryId);
        Task<bool> AdjustPriceAsync(Guid productId, decimal amount, bool isPercentage);
    }
}
