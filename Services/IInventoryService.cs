using PIMS_DOTNET.Models;

namespace PIMS_DOTNET.Services
{
    public interface IInventoryService
    {
        Task<Inventory?> GetByProductIdAsync(Guid productId);
        Task<bool> AdjustInventoryAsync(Guid productId, int quantityChange, string reason, Guid userId);
        Task<IEnumerable<InventoryTransaction>> GetTransactionsAsync(Guid productId);
        Task<IEnumerable<Inventory>> GetLowStockAsync();
        Task<bool> AuditInventoryAsync(Guid productId, int newQuantity, string reason, Guid userId);
    }
}

