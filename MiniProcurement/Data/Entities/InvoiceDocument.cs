namespace MiniProcurement.Data.Entities
{

    public class InvoiceDocument
    {
        public string? Description { get; set; }
        public required string PaymentCardNumber { get; set; }
        public int DocumentBaseId { get; set; }
        public required DocumentBase DocumentBase { get; set; }
    }
}
