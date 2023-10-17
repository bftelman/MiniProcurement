using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Concretes
{
    public class PurchaseRequestDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly BaseDocumentService _documentService;
        private readonly IMapper _mapper;

        public PurchaseRequestDocumentService(IMapper mapper, BaseDocumentService documentService, ApplicationDbContext context)
        {
            _mapper = mapper;
            _documentService = documentService;
            _context = context;
        }

        public async Task CreatePurchaseRequestDocument(int userId, int documentId, PurchaseRequestDocument purchaseRequestDocument)
        {
            var documentBase = await _documentService.GetDocumentById(documentId);

            if (documentBase == null)
            {
                var document = new DocumentBase()
                {
                    DocumentNumber = "PR" + documentId.ToString("D4")[..4],
                    CreatedById = userId
                };

                var documentMapped = _mapper.Map<CreateDocumentDto>(document);

               await _documentService.CreateDocument(documentMapped);
            }

            documentBase!.PurchaseRequests.Add(purchaseRequestDocument);
            _context.PurchaseRequests.Add(purchaseRequestDocument);
            await _context.SaveChangesAsync();
        }

        public async Task<PurchaseRequestDocument> GetPurchaseRequestDocumentById(int id)
        {
            var purchaseRequest = await _context.PurchaseRequests.FindAsync(id) ?? throw new Exception("Purchase request not found");
            return purchaseRequest;
        }

        public async Task UpdatePurchaseRequestDocument(PurchaseRequestDocument purchaseRequestDocument)
        {
            _context.PurchaseRequests.Update(purchaseRequestDocument);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePurchaseRequestDocument(int id)
        {
            var purchaseRequestDocument = await _context.PurchaseRequests.FindAsync(id);
            if (purchaseRequestDocument != null)
            {
                _context.PurchaseRequests.Remove(purchaseRequestDocument);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PurchaseRequestDocument>> GetPurchaseRequests()
        {
            return await _context.PurchaseRequests.ToListAsync();
        }
    }
}
