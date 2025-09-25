namespace PIMS_DOTNET.DTOS
{
    public class ProductUpdateDTO
    {
        public Guid ProductId { get; set; }
        public string SKU { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public IEnumerable<int>? CategoryIds { get; set; }
    }
}
