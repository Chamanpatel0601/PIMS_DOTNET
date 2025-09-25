namespace PIMS_DOTNET.DTOS
{
    public class CategoryUpdateDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
    }
}
