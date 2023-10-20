using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Contracts.InvoiceRequest
{
    public class CreateInvoiceItemDto
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
