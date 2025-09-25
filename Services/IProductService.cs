using PIMS_DOTNET.Models;

namespace PIMS_DOTNET.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> AdjustPriceAsync(Guid productId, decimal adjustment, bool isPercentage);
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);
    }
}
