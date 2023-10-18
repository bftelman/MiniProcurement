using MiniProcurement.Data.Contracts.Document;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Interfaces
{
    public interface IPurchaseRequestDocumentService
    {
        Task CreatePurchaseRequest(CreatePurchaseRequestDto createPurchaseRequestDto);
        Task DeletePurchaseRequestDocument(int id);
        Task<PurchaseRequestDocument> GetPurchaseRequestDocumentById(int id);
        Task<IEnumerable<PurchaseRequestDocument>> GetPurchaseRequests();
        Task UpdatePurchaseRequestDocument(PurchaseRequestDocument purchaseRequestDocument);
    }
}