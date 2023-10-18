namespace MiniProcurement.Data.Contracts.Document
{
    public record GetDocumentDto(int Id, string DocumentNumber, DateTime CreatedOn, int CreatedById);
}
