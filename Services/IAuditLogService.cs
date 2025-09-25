
using PIMS_DOTNET.DTOS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PIMS_DOTNET.Services
{
    public interface IAuditLogService
    {
        Task<IEnumerable<AuditLogDTO>> GetAllAsync();
        Task<IEnumerable<AuditLogDTO>> GetByUserIdAsync(Guid userId);
        Task<IEnumerable<AuditLogDTO>> GetByEntityAsync(string entityName, Guid entityId);
    }
}
