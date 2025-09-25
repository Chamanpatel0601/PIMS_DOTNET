using Microsoft.EntityFrameworkCore;
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

        public AuditLogService(AppDbContext context)
        {
            _context = context;
        }

        // Retrieve all audit logs
        public async Task<IEnumerable<AuditLog>> GetAllAsync()
        {
            return await _context.AuditLogs
                .Include(a => a.User)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        // Retrieve audit logs by a specific user
        public async Task<IEnumerable<AuditLog>> GetByUserIdAsync(Guid userId)
        {
            return await _context.AuditLogs
                .Where(a => a.UserId == userId)
                .Include(a => a.User)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        // Retrieve audit logs by entity name and optional entity ID
        public async Task<IEnumerable<AuditLog>> GetByEntityAsync(string entityName, Guid? entityId = null)
        {
            var query = _context.AuditLogs.AsQueryable();

            query = query.Where(a => a.EntityName == entityName);

            if (entityId.HasValue)
                query = query.Where(a => a.EntityId == entityId);

            return await query
                .Include(a => a.User)
                .OrderByDescending(a => a.Timestamp)
                .ToListAsync();
        }

        // Create a new audit log entry
        public async Task<AuditLog> CreateAsync(AuditLog auditLog)
        {
            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
            return auditLog;
        }
    }
}
