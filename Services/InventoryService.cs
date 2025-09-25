using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;

namespace PIMS_DOTNET.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly AppDbContext _context;

        public InventoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Inventory?> GetByProductIdAsync(Guid productId)
        {
            return await _context.Inventories
                .Include(i => i.Product)
                .FirstOrDefaultAsync(i => i.ProductId == productId);
        }

        public async Task<bool> AdjustInventoryAsync(Guid productId, int quantityChange, string reason, Guid userId)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) return false;

            inventory.Quantity += quantityChange;
            inventory.LastUpdated = DateTime.UtcNow;

            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                QuantityChange = quantityChange,
                Reason = reason,
                UserId = userId
            };

            _context.InventoryTransactions.Add(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetTransactionsAsync(Guid productId)
        {
            return await _context.InventoryTransactions
                .Where(t => t.ProductId == productId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Inventory>> GetLowStockAsync()
        {
            return await _context.Inventories
                .Where(i => i.Quantity < i.LowStockThreshold)
                .ToListAsync();
        }

        public async Task<bool> AuditInventoryAsync(Guid productId, int newQuantity, string reason, Guid userId)
        {
            var inventory = await _context.Inventories.FirstOrDefaultAsync(i => i.ProductId == productId);
            if (inventory == null) return false;

            int difference = newQuantity - inventory.Quantity;
            inventory.Quantity = newQuantity;
            inventory.LastUpdated = DateTime.UtcNow;

            var transaction = new InventoryTransaction
            {
                ProductId = productId,
                QuantityChange = difference,
                Reason = reason,
                UserId = userId
            };

            _context.InventoryTransactions.Add(transaction);

            var auditLog = new AuditLog
            {
                UserId = userId,
                Action = "Inventory Audit",
                EntityName = "Inventory",
                EntityId = inventory.InventoryId,
                OldValue = inventory.Quantity.ToString(),
                NewValue = newQuantity.ToString()
            };

            _context.AuditLogs.Add(auditLog);

            await _context.SaveChangesAsync();
            return true;
        }


    }
    }
