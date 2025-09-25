namespace PIMS_DOTNET.DTOS
{
    public class InventoryTransactionDTO
    {
        public Guid TransactionId { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityChange { get; set; }
        public string Reason { get; set; } = null!;
        public Guid? UserId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
