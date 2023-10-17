using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Entities
{
    public class PurchaseRequestDocumentItem
    {
        public int Id { get; set; }
        public required string MaterialName { get; set; }
        public required int Quantity { get; set; }
        public required int Price { get; set; }
        public required UnitOfMeasure UnitOfMeasure { get; set; }
        public ItemStatus ItemStatus {  get; set; }
        public int? PurchaseRequestDocumentId {  get; set; }
        public PurchaseRequestDocument? PurchaseRequestDocument { get; set;}
    }
}
