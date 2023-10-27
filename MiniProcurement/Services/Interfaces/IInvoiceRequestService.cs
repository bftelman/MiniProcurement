using MiniProcurement.Data.Contracts.InvoiceRequest;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Interfaces;

public interface IInvoiceRequestService
{
    Task AddInvoiceItem(int id, CreateInvoiceItemDto createInvoiceItemDto);
    Task CreateInvoiceRequest(User user, CreateInvoiceDto createInvoiceDto);
    Task ProcessInvoiceTransaction(int invoiceId, int purchaseRequestId);
    Task<IEnumerable<GetInvoiceRequestDto>> GetAllInvoiceRequests();
    Task UpdateInvoice(int id, UpdateInvoiceDto updateInvoiceDto);
    Task DeleteInvoice(int id);
    Task DeleteInvoiceItem(int id, int itemId);
    Task UpdateInvoiceItem(int id, int itemId, UpdateInvoiceItemDto updateInvoiceItemDto);
}