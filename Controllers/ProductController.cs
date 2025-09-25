using Microsoft.AspNetCore.Mvc;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/v1/Product
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }

        // GET: api/v1/Product/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        // POST: api/v1/Product
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductCreateDTO dto)
        {
            try
            {
                var product = await _productService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = product.ProductId }, product);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT: api/v1/Product/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ProductDTO>> Update(Guid id, ProductUpdateDTO dto)
        {
            if (id != dto.ProductId) return BadRequest("Product ID mismatch");

            try
            {
                var updated = await _productService.UpdateAsync(dto);
                if (updated == null) return NotFound();
                return Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE: api/v1/Product/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // GET: api/v1/Product/ByCategory/{categoryId}
        [HttpGet("ByCategory/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByCategory(int categoryId)
        {
            var products = await _productService.GetByCategoryAsync(categoryId);
            return Ok(products);
        }

        // PATCH: api/v1/Product/{id}/AdjustPrice
        [HttpPatch("{id:guid}/AdjustPrice")]
        public async Task<IActionResult> AdjustPrice(Guid id, [FromQuery] decimal amount, [FromQuery] bool isPercentage = false)
        {
            var success = await _productService.AdjustPriceAsync(id, amount, isPercentage);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
