using MiniProcurement.Data.Enumerations;

namespace MiniProcurement.Data.Contracts.Document
{
    public record CreatePurchaseRequestDocumentItemDto(string MaterialName, int Quantity, int Price, UnitOfMeasure UnitOfMeasure, ItemStatus ItemStatus);
}
