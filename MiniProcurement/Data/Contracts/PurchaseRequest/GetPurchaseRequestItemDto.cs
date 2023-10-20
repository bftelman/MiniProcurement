using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Contracts.PurchaseRequest
{
    public class GetPurchaseRequestItemDto
    {
        public int Id { get; set; }
        public required string MaterialName { get; set; }
        public required int Quantity { get; set; }
        public required int Price { get; set; }
        public required UnitOfMeasure UnitOfMeasure { get; set; }
        public ItemStatus ItemStatus { get; set; } = ItemStatus.Unused;
    }
}