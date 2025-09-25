using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMS_DOTNET.Models
{
    public class InventoryTransaction
    {
        [Key]
        public Guid TransactionId { get; set; } = Guid.NewGuid();


        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public int QuantityChange { get; set; }


        [Required]
        [MaxLength(255)]
        public string Reason { get; set; } = null!; // e.g., "Sale", "Restock", "Audit"


        [ForeignKey(nameof(User))]
        public Guid? UserId { get; set; }
        public User? User { get; set; }


        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
