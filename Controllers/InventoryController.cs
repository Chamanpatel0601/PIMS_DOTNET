using Microsoft.AspNetCore.Mvc;
using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Services;
using PIMS_DOTNET.Repository;
using Microsoft.EntityFrameworkCore;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;
        private readonly AppDbContext _context;

        public InventoryController(IInventoryService inventoryService, AppDbContext context)
        {
            _inventoryService = inventoryService;
            _context = context;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetAll()
        {
            var inventories = await _inventoryService.GetAllAsync();
            return Ok(inventories);
        }

        // GET: api/Inventory/product/{productId}
        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<InventoryDTO>> GetByProductId(Guid productId)
        {
            var inventory = await _inventoryService.GetByProductIdAsync(productId);
            if (inventory == null) return NotFound();
            return Ok(inventory);
        }

        // POST: api/Inventory/create
        [HttpPost("create")]
        public async Task<IActionResult> CreateInventory([FromBody] InventoryDTO dto)
        {
            if (dto == null) return BadRequest();

            var exists = await _context.Inventories.AnyAsync(i => i.ProductId == dto.ProductId);
            if (exists) return Conflict("Inventory already exists.");

            var inventory = new Inventory
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                WarehouseLocation = dto.WarehouseLocation,
                LowStockThreshold = dto.LowStockThreshold,
                LastUpdated = DateTime.UtcNow
            };

            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return Ok(inventory);
        }

        // POST: api/Inventory/adjust
        [HttpPost("adjust")]
        public async Task<IActionResult> AdjustInventory([FromBody] InventoryAdjustDTO dto)
        {
            var success = await _inventoryService.AdjustInventoryAsync(dto);
            if (!success) return NotFound("Product not found in inventory.");
            return Ok("Inventory adjusted successfully.");
        }

        // GET: api/Inventory/lowstock?threshold=5
        [HttpGet("lowstock")]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetLowStock([FromQuery] int threshold = 10)
        {
            var lowStockItems = await _inventoryService.GetLowStockAsync(threshold);
            return Ok(lowStockItems);
        }

        // POST: api/Inventory/audit
        [HttpPost("audit")]
        public async Task<IActionResult> AuditInventory([FromQuery] Guid productId, [FromQuery] int newQuantity, [FromQuery] string reason, [FromQuery] Guid userId)
        {
            var success = await _inventoryService.AuditInventoryAsync(productId, newQuantity, reason, userId);
            if (!success) return NotFound("Product not found in inventory.");
            return Ok("Inventory audit completed successfully.");
        }

        // GET: api/Inventory/transactions/{productId}
        [HttpGet("transactions/{productId:guid}")]
        public async Task<ActionResult<IEnumerable<InventoryTransactionDTO>>> GetTransactions(Guid productId)
        {
            var transactions = await _inventoryService.GetTransactionsAsync(productId);
            return Ok(transactions);
        }
    }
}
