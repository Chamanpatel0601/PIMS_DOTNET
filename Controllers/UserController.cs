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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // -------------------- GET all users --------------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // -------------------- GET user by ID --------------------
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDTO>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        // -------------------- POST: Register --------------------
        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserRegisterDTO dto)
        {
            try
            {
                var user = await _userService.RegisterAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // -------------------- POST: Authenticate/Login --------------------
        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login([FromBody] UserLoginDTO dto)
        {
            var user = await _userService.AuthenticateAsync(dto);
            if (user == null) return Unauthorized(new { message = "Invalid username or password" });
            return Ok(user);
        }

        // -------------------- PUT: Update user --------------------
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserDTO>> Update(Guid id, [FromBody] UserRegisterDTO dto)
        {
            try
            {
                var updatedUser = await _userService.UpdateAsync(id, dto);
                if (updatedUser == null) return NotFound();
                return Ok(updatedUser);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // -------------------- DELETE: Delete user --------------------
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
