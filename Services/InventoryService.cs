using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            _context.InventoryTransactions.Add(new InventoryTransaction
            {
                ProductId = productId,
                QuantityChange = quantityChange,
                Reason = reason,
                UserId = userId
            });

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

            int oldQuantity = inventory.Quantity;
            inventory.Quantity = newQuantity;
            inventory.LastUpdated = DateTime.UtcNow;

            _context.InventoryTransactions.Add(new InventoryTransaction
            {
                ProductId = productId,
                QuantityChange = newQuantity - oldQuantity,
                Reason = reason,
                UserId = userId
            });

            _context.AuditLogs.Add(new AuditLog
            {
                UserId = userId,
                Action = "Inventory Audit",
                EntityName = "Inventory",
                EntityId = inventory.InventoryId,
                OldValue = oldQuantity.ToString(),
                NewValue = newQuantity.ToString()
            });

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
