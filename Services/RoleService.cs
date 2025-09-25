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

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleDTO>>(roles);
        }

        public async Task<RoleDTO?> GetByIdAsync(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            return role == null ? null : _mapper.Map<RoleDTO>(role);
        }
    }
}
