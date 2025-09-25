using Microsoft.AspNetCore.Mvc;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;

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

        // GET: api/v1/Role
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(roles);
        }

        // GET: api/v1/Role/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RoleDTO>> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(role);
        }
    }
}
