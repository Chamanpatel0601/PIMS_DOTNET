using PIMS_DOTNET.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLog>> GetAllAsync();
        Task<IEnumerable<AuditLog>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<AuditLog>> GetByEntityAsync(string entityName, Guid? entityId = null);
        Task<AuditLog> CreateAsync(AuditLog auditLog);
    }
}
