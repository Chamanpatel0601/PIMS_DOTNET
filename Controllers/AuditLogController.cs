using Microsoft.AspNetCore.Mvc;

using PIMS_DOTNET.DTOS;
using PIMS_DOTNET.Services;

namespace PIMS_DOTNET.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditLogController : ControllerBase
    {
        private readonly IAuditLogService _auditLogService;

        public AuditLogController(IAuditLogService auditLogService)
        {
            _auditLogService = auditLogService;
        }

        // GET: api/v1/AuditLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLogDTO>>> GetAll()
        {
            var logs = await _auditLogService.GetAllAsync();
            return Ok(logs);
        }

        // GET: api/v1/AuditLog/user/{userId}
        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<IEnumerable<AuditLogDTO>>> GetByUserId(Guid userId)
        {
            var logs = await _auditLogService.GetByUserIdAsync(userId);
            return Ok(logs);
        }

        // GET: api/v1/AuditLog/entity/{entityName}/{entityId}
        [HttpGet("entity/{entityName}/{entityId:guid}")]
        public async Task<ActionResult<IEnumerable<AuditLogDTO>>> GetByEntity(string entityName, Guid entityId)
        {
            var logs = await _auditLogService.GetByEntityAsync(entityName, entityId);
            return Ok(logs);
        }
    }
}
