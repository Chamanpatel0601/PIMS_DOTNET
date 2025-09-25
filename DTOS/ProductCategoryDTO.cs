namespace PIMS_DOTNET.DTOS
{
    public class ProductCategoryDTO
    {
        public Guid ProductId { get; set; }
        public int CategoryId { get; set; }

        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
    }
}
