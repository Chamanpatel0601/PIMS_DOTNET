namespace PIMS_DOTNET.DTOS
{
    public class InventoryAdjustDTO
    {
        public Guid ProductId { get; set; }
        public int QuantityChange { get; set; }
        public string Reason { get; set; } = null!;
        public Guid UserId { get; set; }
    }
}
