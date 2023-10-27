using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Contracts.PurchaseRequest;

public record CreatePurchaseRequestItemDto(string MaterialName, int Quantity, int Price, UnitOfMeasure UnitOfMeasure);