using MiniProcurement.Data.Contracts.PurchaseRequest;

namespace MiniProcurement.Services.Interfaces
{
    public interface IPurchaseRequestService
    {
        Task CreatePurchaseRequest(CreatePurchaseRequestDto createPurchaseRequestDto);
        Task DeletePurchaseRequest(int id);
        Task<GetPurchaseRequestDto> GetPurchaseRequestById(int id);
        Task<IEnumerable<GetPurchaseRequestDto>> GetPurchaseRequests();
        Task UpdatePurchaseRequest(int id, UpdatePurchaseRequestDto updatePurchaseRequestDto);

        Task AddPurchaseRequestItem(int id, CreatePurchaseRequestItemDto createPurchaseRequestItemDto);
        Task<IEnumerable<GetPurchaseRequestItemDto>> GetPurchaseRequestItems(int id);
        Task<GetPurchaseRequestItemDto> GetPurchaseRequestItem(int id, int itemId);
        Task DeletePurchaseRequestItem(int id, int itemId);
        Task UpdatePurchaseRequestItem(int id, int itemId, UpdatePurchaseRequestItemDto updatePurchaseDocumentItem);
    }
}