using PIMS_DOTNET.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid productId);
        Task<Product> CreateAsync(Product product, IEnumerable<int> categoryIds);
        Task<Product?> UpdateAsync(Product product, IEnumerable<int> categoryIds);
        Task<bool> DeleteAsync(Guid productId);

        Task<bool> AdjustPriceAsync(Guid productId, decimal adjustment, bool isPercentage);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
        Task<bool> IsSkuUniqueAsync(string sku, Guid? productId = null);
    }
}
