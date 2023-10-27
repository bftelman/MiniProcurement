using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Contracts.PurchaseRequest;

public class UpdatePurchaseRequestItemDto
{
    public required string MaterialName { get; set; }
    public int Quantity { get; set; }
    public int Price { get; set; }
    public UnitOfMeasure UnitOfMeasure { get; set; }
}