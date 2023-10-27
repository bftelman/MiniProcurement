namespace MiniProcurement.Data.Contracts.PurchaseRequest;

public record CreatePurchaseRequestDto(string DocumentNumber, string DeliveryAddress,
    string Description);