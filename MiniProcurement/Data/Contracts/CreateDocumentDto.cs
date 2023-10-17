using MiniProcurement.Data.Entities;

namespace MiniProcurement.Data.Contracts
{
    public record CreateDocumentDto(string DocumentNumber, int CreatedById);
}
