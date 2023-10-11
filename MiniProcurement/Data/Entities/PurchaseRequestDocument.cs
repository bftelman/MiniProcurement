namespace MiniProcurement.Data.Entities;
public class PurchaseRequestDocument
{
    public int Id { get; set; }
    public string DeliveryAddress { get; set; } = string.Empty;
    public string? Description { get; set; }
}
