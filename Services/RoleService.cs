using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class RoleService : IRoleService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RoleService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Get all roles
        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }

        // Get role by ID
        public async Task<RoleDTO?> GetByIdAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return role == null ? null : _mapper.Map<RoleDTO>(role);
        }

        // Create role
        public async Task<RoleDTO> CreateAsync(RoleDTO dto)
        {
            var role = _mapper.Map<Role>(dto);
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDTO>(role);
        }

        // Update role
        public async Task<RoleDTO?> UpdateAsync(RoleDTO dto)
        {
            var role = await _context.Roles.FindAsync(dto.RoleId);
            if (role == null) return null;

            role.RoleName = dto.RoleName;
            role.Description = dto.Description;

            await _context.SaveChangesAsync();
            return _mapper.Map<RoleDTO>(role);
        }

        // Delete role
        public async Task<bool> DeleteAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null) return false;

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
