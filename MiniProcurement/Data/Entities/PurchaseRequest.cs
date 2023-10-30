namespace MiniProcurement.Data.Entities;

public class PurchaseRequest
{
    public List<PurchaseRequestItem> PurchaseRequestItems { get; set; }
    public required string DeliveryAddress { get; set; }
    public string? Description { get; set; }
    public int DocumentId { get; set; }
    public Document Document { get; set; }
}