namespace MiniProcurement.Data.Contracts.InvoiceRequest;

public class UpdateInvoiceDto
{
    public required string DocumentNumber { get; set; }
    public DateTime UpdatedOn { get; set; } = DateTime.Now;
}