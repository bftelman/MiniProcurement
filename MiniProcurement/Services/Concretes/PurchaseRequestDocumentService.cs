using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.Document;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class PurchaseRequestDocumentService : IPurchaseRequestDocumentService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseRequestDocumentService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task CreatePurchaseRequest(CreatePurchaseRequestDto createPurchaseRequestDto)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == createPurchaseRequestDto.CreatedById)
                                     ?? throw new Exception("User not found. Please provide a valid id");

            if (user.Roles != null && user.Roles.Any(r => r.Name == "USER_DEMAND"))
            {
                var document = _mapper.Map<DocumentBase>(createPurchaseRequestDto);

                var purchaseRequest = _mapper.Map<PurchaseRequestDocument>(createPurchaseRequestDto);
                purchaseRequest.DocumentBase = document;

                _context.PurchaseRequests.Add(purchaseRequest);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User doesn't have permissions to create a purchase request");
            }
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


        // Fix add mapping
        public async Task<IEnumerable<PurchaseRequestDocument>> GetPurchaseRequests()
        {
            return await _context.PurchaseRequests.Include(prdoc => prdoc.DocumentBase).ToListAsync();
        }
    }
}
