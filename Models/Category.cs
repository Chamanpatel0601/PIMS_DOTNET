using System.ComponentModel.DataAnnotations;

namespace PIMS_DOTNET.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }


        [Required]
        [MaxLength(100)]
        public string CategoryName { get; set; } = null!;


        [MaxLength(255)]
        public string? Description { get; set; }


        public ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}

