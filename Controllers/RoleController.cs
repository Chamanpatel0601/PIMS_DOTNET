using Microsoft.AspNetCore.Mvc;
using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        // GET: api/Role/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDTO>> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }

        // POST: api/Role
        [HttpPost]
        public async Task<ActionResult<RoleDTO>> Create([FromBody] RoleDTO dto)
        {
            var created = await _roleService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.RoleId }, created);
        }

        // PUT: api/Role/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<RoleDTO>> Update(int id, [FromBody] RoleDTO dto)
        {
            dto.RoleId = id;
            var updated = await _roleService.UpdateAsync(dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        // DELETE: api/Role/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _roleService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
