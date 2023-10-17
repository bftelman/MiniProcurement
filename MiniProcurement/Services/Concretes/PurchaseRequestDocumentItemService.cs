using AutoMapper;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts;
using MiniProcurement.Data.Entities;

namespace MiniProcurement.Services.Concretes
{
    public class PurchaseRequestDocumentItemService
    {
        private ApplicationDbContext _context;
        private IMapper _mapper;

        public PurchaseRequestDocumentItemService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreatePurchaseRequestDocumentItem(CreatePurchaseRequestDocumentItemDto createPurchaseRequestDocumentItemDto)
        {
            var prdi = _mapper.Map<PurchaseRequestDocumentItem>(createPurchaseRequestDocumentItemDto);
            await _context.PurchaseRequestDocumentItems.AddAsync(prdi);
            await _context.SaveChangesAsync();
        }

        public async Task<PurchaseRequestDocumentItem> GetPurchaseRequestDocumentItemById(int id)
        {
            var prdi = await _context.PurchaseRequestDocumentItems.FindAsync(id) ?? throw new Exception("Product item not found");
            return prdi;
        }

        public async Task UpdatePurchaseRequestDocumentItem(PurchaseRequestDocumentItem purchaseRequestDocumentItem)
        {
            _context.PurchaseRequestDocumentItems.Update(purchaseRequestDocumentItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePurchaseRequestDocumentItem(int id)
        {
            var purchaseRequestDocumentItem = await _context.PurchaseRequestDocumentItems.FindAsync(id);
            if (purchaseRequestDocumentItem != null)
            {
                _context.PurchaseRequestDocumentItems.Remove(purchaseRequestDocumentItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}
