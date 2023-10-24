using MiniProcurement.Data.Enumerations;
using System.Text.Json.Serialization;

namespace MiniProcurement.Data.Entities
{
    public class InvoiceRequestItem
    {
        public int Id { get; set; }
        public int PurchaseRequestItemId { get; set; }
        public PurchaseRequestItem PurchaseRequestItem { get; set; }
        public int Quantity { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public int InvoiceRequestId { get; set; }

        [JsonIgnore]
        public InvoiceRequest InvoiceRequest { get; set; }
    }
}
