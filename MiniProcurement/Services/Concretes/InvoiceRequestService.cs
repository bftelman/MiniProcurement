using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.InvoiceRequest;
using MiniProcurement.Data.Entities;
using MiniProcurement.Data.Enumerations;
using MiniProcurement.Exceptions;
using MiniProcurement.Resources.Localization;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class InvoiceRequestService : IInvoiceRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ExceptionLoc> _localizer;

        public InvoiceRequestService(ApplicationDbContext context, IMapper mapper, IStringLocalizer<ExceptionLoc> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }


        public async Task CreateInvoiceRequest(CreateInvoiceDto createInvoiceDto)
        {
            var user = await _context.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(u => u.Id == createInvoiceDto.CreatedById)
                           ?? throw new NotFoundException(_localizer["UserNotFound"]);

            if (user.Roles.Any(r => r.Name == "USER_SUPPLY"))
            {
                var document = _mapper.Map<Document>(createInvoiceDto);

                var invoiceRequest = _mapper.Map<InvoiceRequest>(createInvoiceDto);
                invoiceRequest.Document = document;

                _context.InvoiceRequests.Add(invoiceRequest);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotAuthorizedException(_localizer["NoInvoiceRights"]);
            }
        }


        public async Task AddInvoiceItem(int id, CreateInvoiceItemDto createInvoiceItemDto)
        {
            var invoice = await _context.InvoiceRequests
                                        .Include(inv => inv.InvoiceRequestItems)
                                        .ThenInclude(inv => inv.PurchaseRequestItem)
                                        .FirstOrDefaultAsync(item => item.DocumentId == id)
                                        ?? throw new NotFoundException(_localizer["InvNotFound"]);

            var item = _mapper.Map<InvoiceRequestItem>(createInvoiceItemDto);

            var prItem = await _context.PurchaseRequestItems.FindAsync(createInvoiceItemDto.ItemId);

            if (invoice.InvoiceRequestItems is [])
            {
                invoice.InvoiceRequestItems.Add(item);
            }
            else
            {
                var firstItem = invoice.InvoiceRequestItems.First();

                if (firstItem.PurchaseRequestItem.PurchaseRequestId == prItem.PurchaseRequestId)
                    invoice.InvoiceRequestItems.Add(item);

                else throw new Exception("Added items should be from the same purchase request!");
            }

            await _context.SaveChangesAsync();
        }

        public async Task ProcessInvoiceTransaction(int invoiceId, int purchaseRequestId)
        {
            var invoiceRequest = await _context.InvoiceRequests
                                               .Include(inv => inv.InvoiceRequestItems)
                                               .FirstOrDefaultAsync(inv => inv.DocumentId == invoiceId)
                                               ?? throw new NotFoundException(_localizer["InvNotFound"]);

            var purchaseRequest = await _context.PurchaseRequests
                                                .Include(pr => pr.PurchaseRequestItems)
                                                .FirstOrDefaultAsync(pr => pr.DocumentId == purchaseRequestId)
                                                ?? throw new NotFoundException(_localizer["PrNotFound"]);

            foreach (var item in invoiceRequest.InvoiceRequestItems)
            {
                var correspondingPurchaseItem = purchaseRequest.PurchaseRequestItems.FirstOrDefault(p => p.Id == item.PurchaseRequestItemId)
                                                                                    ?? throw new NotFoundException(_localizer["PrInvNotFound"]);

                var orderedQuantity = await _context.InvoiceRequestItems
                    .Where(invItem => invItem.PurchaseRequestItemId == item.PurchaseRequestItemId)
                    .SumAsync(invItem => invItem.Quantity);

                var amount = correspondingPurchaseItem.Quantity - orderedQuantity;

                if (amount < 0)
                {
                    throw new Exception(_localizer["NotEnoughItems"]);
                }

                if (amount == 0)
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
            var item = await _context.InvoiceRequestItems.Where(item => item.Id == itemId).FirstOrDefaultAsync()
                                                          ?? throw new NotFoundException(_localizer["InvItemNotFound"]);

            _mapper.Map(updateInvoiceItemDto, item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoiceItem(int id, int itemId)
        {
            var item = await _context.InvoiceRequestItems.Where(item => item.Id == itemId).FirstOrDefaultAsync()
                                                        ?? throw new NotFoundException(_localizer["InvItemNotFound"]);


            _context.InvoiceRequestItems.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteInvoice(int id)
        {
            var invoiceRequest = await _context.InvoiceRequests
                                             .FirstOrDefaultAsync(inv => inv.DocumentId == id)
                                             ?? throw new NotFoundException(_localizer["InvNotFound"]);

            _context.InvoiceRequests.Remove(invoiceRequest);
        }

        public async Task UpdateInvoice(int id, UpdateInvoiceDto updateInvoiceDto)
        {
            var invoice = await _context.Departments.FindAsync(id) ?? throw new NotFoundException(_localizer["InvNotFound"]);
            _mapper.Map(updateInvoiceDto, invoice);
            await _context.SaveChangesAsync();
        }
    }

}
