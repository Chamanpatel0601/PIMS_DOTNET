using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMS_DOTNET.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();


        [Required]
        [MaxLength(100)]
        public string Username { get; set; } = null!;


        [Required]
        [MaxLength(255)]
        public string Email { get; set; } = null!;


        [Required]
        public byte[] PasswordHash { get; set; } = null!;


        [Required]
        public byte[] PasswordSalt { get; set; } = null!;


        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;


        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;


        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();
    }
}

