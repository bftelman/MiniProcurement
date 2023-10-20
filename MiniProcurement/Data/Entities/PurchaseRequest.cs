namespace MiniProcurement.Data.Entities;
public class PurchaseRequest
{
    public required string DeliveryAddress { get; set; }
    public string? Description { get; set; }

    public List<PurchaseRequestItem> PurchaseRequestItems = new();

    public int DocumentId { get; set; }
    public Document Document { get; set; } = null!;
}
