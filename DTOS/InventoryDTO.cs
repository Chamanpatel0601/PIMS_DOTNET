namespace PIMS_DOTNET.DTOS
{
    public class InventoryDTO
    {
        public Guid InventoryId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string? WarehouseLocation { get; set; }
        public int LowStockThreshold { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
