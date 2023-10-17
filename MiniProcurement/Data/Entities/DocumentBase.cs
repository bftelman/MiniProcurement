namespace MiniProcurement.Data.Entities
{
    // This entity is base class for both document types and has
    // fields used by both of them
    public class DocumentBase
    {
        public int Id { get; set; }
        public required string DocumentNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; } = null!;

        public List<PurchaseRequestDocument> PurchaseRequests { get; set; } = new List<PurchaseRequestDocument>();
        public List<InvoiceDocument> InvoiceRequests { get; set; } = new List<InvoiceDocument>();
    }
}
