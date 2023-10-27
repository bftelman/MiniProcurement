namespace MiniProcurement.Data.Entities;

public class InvoiceRequest
{
    public string? Description { get; set; }
    public required string PaymentCardNumber { get; set; }
    public int DocumentId { get; set; }
    public required Document Document { get; set; }

    public required List<InvoiceRequestItem> InvoiceRequestItems { get; set; }
}