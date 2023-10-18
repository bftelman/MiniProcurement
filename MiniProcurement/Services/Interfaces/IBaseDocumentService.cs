using MiniProcurement.Data.Contracts.Document;

namespace MiniProcurement.Services.Interfaces
{
    public interface IBaseDocumentService
    {
        Task CreateDocument(CreateDocumentDto createDocumentDto);
        Task DeleteDocument(int id);
        Task<IEnumerable<GetDocumentDto>> GetAllDocuments();
        Task<GetDocumentDto> GetDocumentById(int id);
        Task UpdateDocument(int id, UpdateDocumentDto updateDocumentDto);
    }
}