using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;

namespace PIMS_DOTNET.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InventoryService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InventoryDTO?> GetByProductIdAsync(Guid productId)
        {
            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.ProductId == productId);
            return inventory == null ? null : _mapper.Map<InventoryDTO>(inventory);
        }

        public async Task<IEnumerable<InventoryDTO>> GetAllAsync()
        {
            var inventories = await _context.Inventories
                .Include(i => i.Product)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<bool> AdjustInventoryAsync(InventoryAdjustDTO dto)
        {
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(i => i.ProductId == dto.ProductId);

            if (inventory == null) return false;

            inventory.Quantity += dto.QuantityChange;
            inventory.LastUpdated = DateTime.UtcNow;

            var transaction = new InventoryTransaction
            {
                ProductId = dto.ProductId,
                QuantityChange = dto.QuantityChange,
                Reason = dto.Reason,
                UserId = dto.UserId
            };

            _context.InventoryTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InventoryDTO>> GetLowStockAsync(int threshold = 10)
        {
            var inventories = await _context.Inventories
                .Where(i => i.Quantity <= threshold)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InventoryDTO>>(inventories);
        }

        public async Task<bool> AuditInventoryAsync(Guid productId, int newQuantity, string reason, Guid userId)
        {
            var inventory = await _context.Inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) return false;

            int difference = newQuantity - inventory.Quantity;
            inventory.Quantity = newQuantity;
            inventory.LastUpdated = DateTime.UtcNow;

            // Log transaction
            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                QuantityChange = difference,
                Reason = reason,
                UserId = userId
            };
            _context.InventoryTransactions.Add(transaction);

            // Log audit
            var audit = new AuditLog
            {
                UserId = userId,
                Action = "Inventory Audit",
                EntityName = "Inventory",
                EntityId = inventory.InventoryId,
                OldValue = (inventory.Quantity - difference).ToString(),
                NewValue = newQuantity.ToString()
            };
            _context.AuditLogs.Add(audit);

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetTransactionsAsync(Guid productId)
        {
            var transactions = await _context.InventoryTransactions
                .Where(t => t.ProductId == productId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InventoryTransactionDTO>>(transactions);
        }
    }
}
