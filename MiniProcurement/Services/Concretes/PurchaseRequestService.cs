﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniProcurement.Data.Contexts;
using MiniProcurement.Data.Contracts.InvoiceRequest;
using MiniProcurement.Data.Contracts.PurchaseRequest;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Services.Concretes
{
    public class PurchaseRequestService : IPurchaseRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseRequestService(IMapper mapper, ApplicationDbContext context)
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
                var document = _mapper.Map<Document>(createPurchaseRequestDto);

                var purchaseRequest = _mapper.Map<PurchaseRequest>(createPurchaseRequestDto);
                purchaseRequest.Document = document;

                _context.PurchaseRequests.Add(purchaseRequest);

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User doesn't have permissions to create a purchase request");
            }
        }

        public async Task<GetPurchaseRequestDto> GetPurchaseRequestById(int id)
        {
            var res = await _context.PurchaseRequests
                                                .Include(prdoc => prdoc.Document)
                                                .FirstOrDefaultAsync(prdoc => prdoc.DocumentId == id)
                                                ?? throw new Exception("Purchase request not found");
            var purchaseRequest = _mapper.Map<GetPurchaseRequestDto>(res);
            return purchaseRequest;
        }

        public async Task UpdatePurchaseRequest(int id, UpdatePurchaseRequestDto updatePurchaseRequestDto)
        {
            var pr = await _context.Departments.FindAsync(id) ?? throw new Exception("Purchase request not found. Please provide a valid id");
            _mapper.Map(updatePurchaseRequestDto, pr);
            await _context.SaveChangesAsync();
        }
        public async Task DeletePurchaseRequest(int id)
        {
            var purchaseRequestDocument = await _context.PurchaseRequests.FindAsync(id);
            if (purchaseRequestDocument != null)
            {
                _context.PurchaseRequests.Remove(purchaseRequestDocument);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<GetPurchaseRequestDto>> GetPurchaseRequests()
        {
            var res = await _context.PurchaseRequests.Include(prdoc => prdoc.Document).ToListAsync();
            var purchaseRequests = _mapper.Map<IEnumerable<GetPurchaseRequestDto>>(res);
            return purchaseRequests;
        }

        public async Task AddPurchaseRequestItem(int id, CreatePurchaseRequestItemDto createPurchaseRequestItemDto)
        {
            var prItem = _mapper.Map<PurchaseRequestItem>(createPurchaseRequestItemDto);
            var pr = await _context.PurchaseRequests.FindAsync(id) ?? throw new Exception("Purchase request not found. Please provide a valid id.");


            if (pr.PurchaseRequestItems == null)
            {
                pr.PurchaseRequestItems = new List<PurchaseRequestItem> { prItem };
            }
            else
            {
                pr.PurchaseRequestItems.Add(prItem);
            }

            _context.PurchaseRequestItems.Add(prItem);

            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<GetPurchaseRequestItemDto>> GetPurchaseRequestItems(int id)
        {
            var prItems = await _context.PurchaseRequestItems.Where(prItem => prItem.PurchaseRequestId == id).ToListAsync();
            var res = _mapper.Map<IEnumerable<GetPurchaseRequestItemDto>>(prItems);
            return res;
        }

        public async Task<GetPurchaseRequestItemDto> GetPurchaseRequestItem(int id, int itemId)
        {
            var prItem = await _context.PurchaseRequestItems.Where(prItem => prItem.PurchaseRequestId == id).FirstOrDefaultAsync(item => item.Id == itemId);
            var res = _mapper.Map<GetPurchaseRequestItemDto>(prItem);
            return res;
        }

        public async Task DeletePurchaseRequestItem(int id, int itemId)
        {
            var prItem = await _context.PurchaseRequestItems
                                       .Where(prItem => prItem.PurchaseRequestId == id)
                                       .FirstOrDefaultAsync(item => item.Id == itemId)
                                       ?? throw new Exception("Purchase request item not found. Please provide a valid id");

            _context.PurchaseRequestItems.Remove(prItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePurchaseRequestItem(int id, int itemId, UpdatePurchaseRequestItemDto updatePurchaseDocumentItem)
        {
            var prItem = await _context.PurchaseRequestItems
                                       .Where(prItem => prItem.PurchaseRequestId == id)
                                       .FirstOrDefaultAsync(item => item.Id == itemId)
                                       ?? throw new Exception("Purchase request item not found. Please provide a valid id");

            _mapper.Map(updatePurchaseDocumentItem, prItem);
            await _context.SaveChangesAsync();
        }
    }
}