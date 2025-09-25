using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMS_DOTNET.Models
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId { get; set; } = Guid.NewGuid();


        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;


        public int Quantity { get; set; } = 0;


        [MaxLength(100)]
        public string? WarehouseLocation { get; set; }


        public int LowStockThreshold { get; set; } = 10;


        public DateTime LastUpdated { get; set; } = DateTime.UtcNow;


        public ICollection<InventoryTransaction> Transactions { get; set; } = new List<InventoryTransaction>();
    }
}

