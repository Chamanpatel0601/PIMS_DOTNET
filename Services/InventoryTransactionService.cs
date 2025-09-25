using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class InventoryTransactionService : IInventoryTransactionService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InventoryTransactionService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetAllAsync()
        {
            var transactions = await _context.InventoryTransactions
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InventoryTransactionDTO>>(transactions);
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetByProductIdAsync(Guid productId)
        {
            var transactions = await _context.InventoryTransactions
                .Where(t => t.ProductId == productId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InventoryTransactionDTO>>(transactions);
        }

        public async Task<IEnumerable<InventoryTransactionDTO>> GetByUserIdAsync(Guid userId)
        {
            var transactions = await _context.InventoryTransactions
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.TransactionDate)
                .ToListAsync();
            return _mapper.Map<IEnumerable<InventoryTransactionDTO>>(transactions);
        }

        public async Task<InventoryTransactionDTO?> GetByIdAsync(Guid transactionId)
        {
            var transaction = await _context.InventoryTransactions
                .FirstOrDefaultAsync(t => t.TransactionId == transactionId);
            return transaction == null ? null : _mapper.Map<InventoryTransactionDTO>(transaction);
        }
    }
}
