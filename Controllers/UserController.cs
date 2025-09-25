using Microsoft.AspNetCore.Mvc;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // POST: api/v1/User/register
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserRegisterDTO dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        // POST: api/v1/User/login
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserLoginDTO dto)
        {
            var user = await _userService.AuthenticateAsync(dto);
            if (user == null) return Unauthorized(new { message = "Invalid credentials" });
            return Ok(user);
        }

        // GET: api/v1/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // GET: api/v1/User/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }
    }
}
