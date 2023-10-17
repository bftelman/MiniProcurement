using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Concretes
{
    public class InvoiceDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly BaseDocumentService _documentService;

        public InvoiceDocumentService(ApplicationDbContext context, BaseDocumentService documentService)
        {
            _context = context;
            _documentService = documentService;
        }

        public async Task CreateInvoiceDocument(int documentId, InvoiceDocument invoiceDocument)
        {
            var documentBase = await _documentService.GetDocumentById(documentId);

            if (documentBase == null)
            {
                throw new Exception("DocumentBase not found. Please provide a valid documentId.");
            }

            documentBase.InvoiceRequests.Add(invoiceDocument);
            _context.Invoices.Add(invoiceDocument);
            await _context.SaveChangesAsync();
        }

        public async Task<InvoiceDocument> GetInvoiceDocumentById(int id)
        {
            var invoice = await _context.Invoices.FindAsync(id) ?? throw new Exception("Invoice not found exception");
            return invoice;
        }

        public async Task UpdateInvoiceDocument(InvoiceDocument invoiceDocument)
        {
            _context.Invoices.Update(invoiceDocument);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceDocument(int id)
        {
            var invoiceDocument = await _context.Invoices.FindAsync(id);
            if (invoiceDocument != null)
            {
                _context.Invoices.Remove(invoiceDocument);
                await _context.SaveChangesAsync();
            }
        }
    }

}
