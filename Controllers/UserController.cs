//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using PIMS_DOTNET.DTOS;
//using PIMS_DOTNET.Services;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace PIMS_DOTNET.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class UserController : ControllerBase
//    {
//        private readonly IUserService _userService;
//        private readonly IConfiguration _configuration;

//        public UserController(IUserService userService, IConfiguration configuration)
//        {
//            _userService = userService;
//            _configuration = configuration;
//        }

//        // -------------------- GET all users --------------------
//        [Authorize(Roles = "Admin")]
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
//        {
//            var users = await _userService.GetAllAsync();
//            return Ok(users);
//        }

//        // -------------------- GET user by ID --------------------
//        [Authorize]
//        [HttpGet("{id:guid}")]
//        public async Task<ActionResult<UserDTO>> GetById(Guid id)
//        {
//            var user = await _userService.GetByIdAsync(id);
//            if (user == null) return NotFound();
//            return Ok(user);
//        }

//        // -------------------- POST: Register --------------------
//        [HttpPost("register")]
//        public async Task<ActionResult<UserDTO>> Register([FromBody] UserRegisterDTO dto)
//        {
//            try
//            {
//                var user = await _userService.RegisterAsync(dto);
//                return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
//            }
//            catch (InvalidOperationException ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        // -------------------- POST: Authenticate/Login --------------------
//        [HttpPost("login")]
//        public async Task<ActionResult<object>> Login([FromBody] UserLoginDTO dto)
//        {
//            var user = await _userService.AuthenticateAsync(dto);
//            if (user == null) return Unauthorized(new { message = "Invalid username or password" });

//            // Generate JWT Token
//            var token = GenerateJwtToken(user);

//            return Ok(new
//            {
//                token,
//                user
//            });
//        }

//        // -------------------- PUT: Update user --------------------
//        [Authorize]
//        [HttpPut("{id:guid}")]
//        public async Task<ActionResult<UserDTO>> Update(Guid id, [FromBody] UserRegisterDTO dto)
//        {
//            try
//            {
//                var updatedUser = await _userService.UpdateAsync(id, dto);
//                if (updatedUser == null) return NotFound();
//                return Ok(updatedUser);
//            }
//            catch (InvalidOperationException ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        // -------------------- DELETE: Delete user --------------------
//        [Authorize(Roles = "Admin")]
//        [HttpDelete("{id:guid}")]
//        public async Task<ActionResult> Delete(Guid id)
//        {
//            var deleted = await _userService.DeleteAsync(id);
//            if (!deleted) return NotFound();
//            return NoContent();
//        }

//        // --------------------JWT Token Generation --------------------
//        private string GenerateJwtToken(UserDTO user)
//        {
//            var jwtSettings = _configuration.GetSection("Jwt");
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var claims = new List<Claim>
//            {
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
//                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
//                new Claim(ClaimTypes.Role, user.RoleName ?? "User") 
//            };

//            var token = new JwtSecurityToken(
//                issuer: jwtSettings["Issuer"],
//                audience: jwtSettings["Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddHours(2),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        // -------------------- GET all users --------------------
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        // -------------------- GET user by ID --------------------
        [Authorize]
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
        public async Task<ActionResult<object>> Login([FromBody] UserLoginDTO dto)
        {
            var user = await _userService.AuthenticateAsync(dto);
            if (user == null) return Unauthorized(new { message = "Invalid username or password" });

            // Generate JWT Token
            var token = GenerateJwtToken(user);

            return Ok(new
            {
                token,
                user
            });
        }

        // -------------------- PUT: Update user --------------------
        [Authorize]
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
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _userService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }

        // --------------------JWT Token Generation --------------------
        private string GenerateJwtToken(UserDTO user)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Normalize role name -> Always "Admin" if Administrator, etc.
            string normalizedRole = (user.RoleName ?? "User").ToLower() switch
            {
                "administrator" => "Admin",
                "admin" => "Admin",
                _ => "User"
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(ClaimTypes.Role, normalizedRole)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
