using AutoMapper;
using Microsoft.EntityFrameworkCore;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Models;
using PIMS_DOTNET.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public class AuditLogService : IAuditLogService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AuditLogService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuditLogDTO>> GetAllAsync()
        {
            var logs = await _context.AuditLogs.Include(a => a.User).ToListAsync();
            return _mapper.Map<IEnumerable<AuditLogDTO>>(logs);
        }

        public async Task<IEnumerable<AuditLogDTO>> GetByUserIdAsync(Guid userId)
        {
            var logs = await _context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AuditLogDTO>>(logs);
        }

        public async Task<IEnumerable<AuditLogDTO>> GetByEntityAsync(string entityName, Guid entityId)
        {
            var logs = await _context.AuditLogs
                .Include(a => a.User)
                .Where(a => a.EntityName == entityName && a.EntityId == entityId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<AuditLogDTO>>(logs);
        }
    }
}
