using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Contracts.InvoiceRequest
{
    public class GetInvoiceRequestDto
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public required string PaymentCardNumber { get; set; }
        public required List<InvoiceRequestItem> InvoiceRequestItems { get; set; }
    }
}
