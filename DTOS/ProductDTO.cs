namespace PIMS_DOTNET.DTOS
{
    public class ProductDTO
    {

        public Guid ProductId { get; set; }
        public string SKU { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public IEnumerable<CategoryDTO>? Categories { get; set; }
        public InventoryDTO? Inventory { get; set; }
    }
}
