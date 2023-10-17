namespace MiniProcurement.Data.Entities;
public class PurchaseRequestDocument
{
    public required string DeliveryAddress { get; set; }
    public string? Description { get; set; }

    public List<PurchaseRequestDocumentItem> PurchaseRequestDocumentItems = new();

    public int DocumentBaseId {  get; set; }
    public DocumentBase DocumentBase { get; set; } = null!;
}
