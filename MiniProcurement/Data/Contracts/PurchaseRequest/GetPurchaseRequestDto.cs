namespace MiniProcurement.Data.Contracts.PurchaseRequest
{
    public class GetPurchaseRequestDto
    {
        public int Id { get; set; }
        public required string DeliveryAddress { get; set; }
        public string? Description { get; set; }
        public required string DocumentNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedById { get; set; }
    }
}
