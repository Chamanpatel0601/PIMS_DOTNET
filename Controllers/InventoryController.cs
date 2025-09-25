using Microsoft.AspNetCore.Mvc;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: api/v1/Inventory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetAll()
        {
            var inventories = await _inventoryService.GetAllAsync();
            return Ok(inventories);
        }

        // GET: api/v1/Inventory/product/{productId}
        [HttpGet("product/{productId:guid}")]
        public async Task<ActionResult<InventoryDTO>> GetByProductId(Guid productId)
        {
            var inventory = await _inventoryService.GetByProductIdAsync(productId);
            if (inventory == null) return NotFound();
            return Ok(inventory);
        }

        // POST: api/v1/Inventory/adjust
        [HttpPost("adjust")]
        public async Task<IActionResult> AdjustInventory([FromBody] InventoryAdjustDTO dto)
        {
            var success = await _inventoryService.AdjustInventoryAsync(dto);
            if (!success) return NotFound("Product not found in inventory.");
            return Ok("Inventory adjusted successfully.");
        }

        // GET: api/v1/Inventory/lowstock?threshold=5
        [HttpGet("lowstock")]
        public async Task<ActionResult<IEnumerable<InventoryDTO>>> GetLowStock([FromQuery] int threshold = 10)
        {
            var lowStockItems = await _inventoryService.GetLowStockAsync(threshold);
            return Ok(lowStockItems);
        }

        // POST: api/v1/Inventory/audit
        [HttpPost("audit")]
        public async Task<IActionResult> AuditInventory([FromQuery] Guid productId, [FromQuery] int newQuantity, [FromQuery] string reason, [FromQuery] Guid userId)
        {
            var success = await _inventoryService.AuditInventoryAsync(productId, newQuantity, reason, userId);
            if (!success) return NotFound("Product not found in inventory.");
            return Ok("Inventory audit completed successfully.");
        }

        // GET: api/v1/Inventory/transactions/{productId}
        [HttpGet("transactions/{productId:guid}")]
        public async Task<ActionResult<IEnumerable<InventoryTransactionDTO>>> GetTransactions(Guid productId)
        {
            var transactions = await _inventoryService.GetTransactionsAsync(productId);
            return Ok(transactions);
        }
    }
}
