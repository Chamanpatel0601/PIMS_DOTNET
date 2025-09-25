
using PIMS_DOTNET.DTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDTO>> GetAllAsync();
        Task<InventoryDTO?> GetByProductIdAsync(Guid productId);
        Task<bool> AdjustInventoryAsync(InventoryAdjustDTO dto);
        Task<IEnumerable<InventoryDTO>> GetLowStockAsync(int threshold = 10);
        Task<bool> AuditInventoryAsync(Guid productId, int newQuantity, string reason, Guid userId);
        Task<IEnumerable<InventoryTransactionDTO>> GetTransactionsAsync(Guid productId);
    }
}
