namespace MiniProcurement.Data.Contracts.Document
{
    public record CreatePurchaseRequestDto(string DocumentNumber, int CreatedById, string DeliveryAddress, string Description);
}