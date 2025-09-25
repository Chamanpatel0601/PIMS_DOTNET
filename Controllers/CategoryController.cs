using Microsoft.AspNetCore.Mvc;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: api/v1/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        // GET: api/v1/Category/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        // POST: api/v1/Category
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryCreateDTO dto)
        {
            var category = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = category.CategoryId }, category);
        }

        // PUT: api/v1/Category/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Update(int id, CategoryUpdateDTO dto)
        {
            if (id != dto.CategoryId) return BadRequest("Category ID mismatch");

            var updated = await _categoryService.UpdateAsync(dto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

        // DELETE: api/v1/Category/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
