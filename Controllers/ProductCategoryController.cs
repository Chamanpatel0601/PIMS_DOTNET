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
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _service;

        public ProductCategoryController(IProductCategoryService service)
        {
            _service = service;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/ProductCategory/product/{productId}
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetByProductId(Guid productId)
        {
            var result = await _service.GetByProductIdAsync(productId);
            return Ok(result);
        }

        // GET: api/ProductCategory/category/{categoryId}
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<ProductCategoryDTO>>> GetByCategoryId(int categoryId)
        {
            var result = await _service.GetByCategoryIdAsync(categoryId);
            return Ok(result);
        }

        // POST: api/ProductCategory
        [HttpPost]
        public async Task<ActionResult<ProductCategoryDTO>> Add([FromBody] ProductCategoryCreateDTO dto)
        {
            var result = await _service.AddAsync(dto);
            if (result == null) return BadRequest("Mapping already exists.");
            return Ok(result);
        }

        // DELETE: api/ProductCategory/{productId}/{categoryId}
        [HttpDelete("{productId}/{categoryId}")]
        public async Task<IActionResult> Remove(Guid productId, int categoryId)
        {
            var success = await _service.RemoveAsync(productId, categoryId);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
