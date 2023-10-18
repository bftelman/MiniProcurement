namespace MiniProcurement.Data.Contracts.Document
{
    public record UpdateDocumentDto(string DocumentNumber, int CreatedById, DateTime UpdatedOn);
}
