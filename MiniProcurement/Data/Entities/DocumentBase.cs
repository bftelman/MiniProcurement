namespace MiniProcurement.Data.Entities
{
    public class DocumentBase
    {
        public int Id { get; set; }
        public required string DocumentNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public PurchaseRequestDocument? PurchaseRequest { get; set; }
        public InvoiceDocument? InvoiceRequest { get; set; }
    }
}