using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.InvoiceRequest;
using MiniProcurement.Data.Entities;
using MiniProcurement.Data.Enumerations;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class InvoiceRequestService : IInvoiceRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public InvoiceRequestService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task CreateInvoiceRequest(CreateInvoiceDto createInvoiceDto)
        {
            var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == createInvoiceDto.CreatedById)
                           ?? throw new Exception("User not found. Please provide a valid id");

            if (user.Roles != null && user.Roles.Any(r => r.Name == "USER_SUPPLY"))
            {
                var document = _mapper.Map<Document>(createInvoiceDto);

                var invoiceRequest = _mapper.Map<InvoiceRequest>(createInvoiceDto);
                invoiceRequest.Document = document;

                _context.InvoiceRequests.Add(invoiceRequest);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User doesn't have permissions to create a purchase request");
            }
        }


        public async Task AddInvoiceItem(int id, CreateInvoiceItemDto createInvoiceItemDto)
        {
            var invoice = await _context.InvoiceRequests
                                        .Include(inv => inv.InvoiceRequestItems)
                                        .FirstOrDefaultAsync(item => item.DocumentId == id)
                                        ?? throw new Exception("Invoice not found. Please provide a valid id");
            var item = _mapper.Map<InvoiceRequestItem>(createInvoiceItemDto);
            _context.InvoiceRequestItems.Add(item);
            if (invoice.InvoiceRequestItems == null)
            {
                invoice.InvoiceRequestItems = new List<InvoiceRequestItem> { item };
            }
            else
            {
                invoice.InvoiceRequestItems.Add(item);
            }


            await _context.SaveChangesAsync();
        }

        public async Task ProcessInvoiceTransaction(int invoiceId, int purchaseRequestId)
        {
            var invoiceRequest = await _context.InvoiceRequests
                                               .Include(inv => inv.InvoiceRequestItems)
                                               .FirstOrDefaultAsync(inv => inv.DocumentId == invoiceId)
                                               ?? throw new Exception("Invoice not found. Please provide a valid id");

            var purchaseRequest = await _context.PurchaseRequests
                                                .Include(pr => pr.PurchaseRequestItems)
                                                .FirstOrDefaultAsync(pr => pr.DocumentId == purchaseRequestId)
                                                ?? throw new Exception("Purchase request not found. Please provide a valid id");

            foreach (var item in invoiceRequest.InvoiceRequestItems)
            {
                var correspondingPurchaseItem = purchaseRequest.PurchaseRequestItems.FirstOrDefault(p => p.Id == item.ItemId);

                if (correspondingPurchaseItem == null)
                {
                    throw new Exception("Corresponding purchase item not found for the invoice item");
                }

                var remainingQuantity = correspondingPurchaseItem.Quantity - item.Quantity;

                if (remainingQuantity < 0)
                {
                    throw new Exception("Trying to sell more quantity than available in the purchase request");
                }

                correspondingPurchaseItem.Quantity = remainingQuantity;

                if (remainingQuantity == 0)
                {
                    correspondingPurchaseItem.ItemStatus = ItemStatus.Complete;
                }
                else
                {
                    correspondingPurchaseItem.ItemStatus = ItemStatus.PartiallyUsed;
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GetInvoiceRequestDto>> GetAllInvoiceRequests()
        {
            var invoiceRequests = await _context.InvoiceRequests.Include(inv => inv.InvoiceRequestItems).ToListAsync();
            var result = _mapper.Map<IEnumerable<GetInvoiceRequestDto>>(invoiceRequests);
            return result;
        }

        public async Task UpdateInvoiceItem(int id, int itemId, UpdateInvoiceItemDto updateInvoiceItemDto)
        {
            var invoiceRequest = await _context.InvoiceRequests
                                              .Include(inv => inv.InvoiceRequestItems)
                                              .FirstOrDefaultAsync(inv => inv.DocumentId == id)
                                              ?? throw new Exception("Invoice not found. Please provide a valid id");
            var item = invoiceRequest.InvoiceRequestItems.FirstOrDefault(item => item.Id == itemId)
                                                         ?? throw new Exception("Invoice item not found please provide a valid id");
            _mapper.Map(updateInvoiceItemDto, item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceItem(int id, int itemId)
        {
            var invoiceRequest = await _context.InvoiceRequests
                                             .Include(inv => inv.InvoiceRequestItems)
                                             .FirstOrDefaultAsync(inv => inv.DocumentId == id)
                                             ?? throw new Exception("Invoice not found. Please provide a valid id");
            var item = invoiceRequest.InvoiceRequestItems.FirstOrDefault(item => item.Id == itemId)
                                                         ?? throw new Exception("Invoice item not found please provide a valid id");
            _context.InvoiceRequestItems.Remove(item);
        }

        public async Task DeleteInvoice(int id)
        {
            var invoiceRequest = await _context.InvoiceRequests
                                             .Include(inv => inv.InvoiceRequestItems)
                                             .FirstOrDefaultAsync(inv => inv.DocumentId == id)
                                             ?? throw new Exception("Invoice not found. Please provide a valid id");
            _context.InvoiceRequests.Remove(invoiceRequest);
        }

        public async Task UpdateInvoice(int id, UpdateInvoiceDto updateInvoiceDto)
        {
            var invoice = await _context.Departments.FindAsync(id) ?? throw new Exception("Invoice not found. Please provide a valid id");
            _mapper.Map(updateInvoiceDto, invoice);
            await _context.SaveChangesAsync();
        }
    }

}
