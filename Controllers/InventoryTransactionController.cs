using Microsoft.AspNetCore.Mvc;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryTransactionController : ControllerBase
    {
        private readonly IInventoryTransactionService _transactionService;

        public InventoryTransactionController(IInventoryTransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/InventoryTransaction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryTransactionDTO>>> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }

        // GET: api/InventoryTransaction/{transactionId}
        [HttpGet("{transactionId:guid}")]
        public async Task<ActionResult<InventoryTransactionDTO>> GetById(Guid transactionId)
        {
            var transaction = await _transactionService.GetByIdAsync(transactionId);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }

        // GET: api/InventoryTransaction/product/{productId}
        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<IEnumerable<InventoryTransactionDTO>>> GetByProductId(Guid productId)
        {
            var transactions = await _transactionService.GetByProductIdAsync(productId);
            return Ok(transactions);
        }

        // GET: api/InventoryTransaction/user/{userId}
        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<InventoryTransactionDTO>>> GetByUserId(Guid userId)
        {
            var transactions = await _transactionService.GetByUserIdAsync(userId);
            return Ok(transactions);
        }
    }
}
