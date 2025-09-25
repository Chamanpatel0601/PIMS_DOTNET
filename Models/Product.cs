using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMS_DOTNET.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; } = Guid.NewGuid();


        [Required]
        [MaxLength(50)]
        public string SKU { get; set; } = null!; // Unique constraint configured in EF Fluent API


        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = null!;


        public string? Description { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }


        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;


        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
        public Inventory? Inventory { get; set; }
        public ICollection<InventoryTransaction> InventoryTransactions { get; set; } = new List<InventoryTransaction>();
    }
}

