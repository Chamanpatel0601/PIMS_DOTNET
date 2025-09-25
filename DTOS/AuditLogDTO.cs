namespace PIMS_DOTNET.DTOS
{
    public class AuditLogDTO
    {
        public Guid AuditId { get; set; }
        public Guid? UserId { get; set; }
        public string? Username { get; set; }
        public string Action { get; set; } = null!;
        public string? EntityName { get; set; }
        public Guid? EntityId { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
