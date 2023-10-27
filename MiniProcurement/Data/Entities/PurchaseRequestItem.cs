using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Entities;

public class PurchaseRequestItem
{
    public int Id { get; set; }
    public required string MaterialName { get; set; }
    public required int Quantity { get; set; }
    public required int Price { get; set; }
    public required UnitOfMeasure UnitOfMeasure { get; set; }
    public ItemStatus ItemStatus { get; set; } = ItemStatus.Unused;
    public int? PurchaseRequestId { get; set; }
    public PurchaseRequest? PurchaseRequest { get; set; }
    public ICollection<InvoiceRequestItem>? InvoiceRequestItems { get; set; }
}