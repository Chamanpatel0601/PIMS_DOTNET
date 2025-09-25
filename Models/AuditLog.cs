using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMS_DOTNET.Models
{
    public class AuditLog
    {
        [Key]
        public Guid AuditId { get; set; } = Guid.NewGuid();


        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User? User { get; set; }


        [Required]
        [MaxLength(255)]
        public string Action { get; set; } = null!;


        [MaxLength(100)]
        public string? EntityName { get; set; }


        public Guid? EntityId { get; set; }


        public string? OldValue { get; set; }
        public string? NewValue { get; set; }


        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
