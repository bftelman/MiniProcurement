using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Contracts
{
    public record CreatePurchaseRequestDocumentItemDto(string MaterialName, int Quantity, int Price, UnitOfMeasure UnitOfMeasure, ItemStatus ItemStatus);
}
