namespace MiniProcurement.Data.Entities
{
    public class Document
    {
        public int Id { get; set; }
        public required string DocumentNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public PurchaseRequest? PurchaseRequest { get; set; }
        public InvoiceRequest? InvoiceRequest { get; set; }
    }
}