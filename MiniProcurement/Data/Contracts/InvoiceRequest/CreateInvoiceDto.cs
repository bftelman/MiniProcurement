namespace MiniProcurement.Data.Contracts.InvoiceRequest;

public class CreateInvoiceDto
{
    public string? Description { get; set; }
    public required string PaymentCardNumber { get; set; }
    public required string DocumentNumber { get; set; }
}