namespace MiniProcurement.Data.Contracts.PurchaseRequest
{
    public record CreatePurchaseRequestDto(string DocumentNumber, int CreatedById, string DeliveryAddress, string Description);
}