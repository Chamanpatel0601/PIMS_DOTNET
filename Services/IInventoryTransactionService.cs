
using PIMS_DOTNET.DTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IInventoryTransactionService
    {
        Task<IEnumerable<InventoryTransactionDTO>> GetAllAsync();
        Task<IEnumerable<InventoryTransactionDTO>> GetByProductIdAsync(Guid productId);
        Task<IEnumerable<InventoryTransactionDTO>> GetByUserIdAsync(Guid userId);
        Task<InventoryTransactionDTO?> GetByIdAsync(Guid transactionId);
    }
}
