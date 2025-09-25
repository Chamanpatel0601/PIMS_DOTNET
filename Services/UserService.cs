using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Register a new user
        public async Task<UserDTO?> RegisterAsync(UserRegisterDTO dto)
        {
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username || u.Email == dto.Email))
                throw new InvalidOperationException("Username or Email already exists");

            using var hmac = new HMACSHA512();
            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                RoleId = dto.RoleId,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key,
                IsActive = true,
                CreatedDate = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Include Role for DTO mapping
            await _context.Entry(user).Reference(u => u.Role).LoadAsync();

            return _mapper.Map<UserDTO>(user);
        }

        // Authenticate user
        public async Task<UserDTO?> AuthenticateAsync(UserLoginDTO dto)
        {
            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == dto.Username);

            if (user == null) return null;

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return null;
            }

            return _mapper.Map<UserDTO>(user);
        }

        // Get all users
        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        // Get user by ID
        public async Task<UserDTO?> GetByIdAsync(Guid userId)
        {
            var user = await _context.Users.Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            return user == null ? null : _mapper.Map<UserDTO>(user);
        }

        // Update user
        public async Task<UserDTO?> UpdateAsync(Guid userId, UserRegisterDTO dto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            if (await _context.Users.AnyAsync(u => (u.Username == dto.Username || u.Email == dto.Email) && u.UserId != userId))
                throw new InvalidOperationException("Username or Email already exists");

            user.Username = dto.Username;
            user.Email = dto.Email;
            user.RoleId = dto.RoleId;

            if (!string.IsNullOrEmpty(dto.Password))
            {
                using var hmac = new HMACSHA512();
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
                user.PasswordSalt = hmac.Key;
            }

            await _context.SaveChangesAsync();

            // Include Role for DTO mapping
            await _context.Entry(user).Reference(u => u.Role).LoadAsync();

            return _mapper.Map<UserDTO>(user);
        }

        // Delete user
        public async Task<bool> DeleteAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
