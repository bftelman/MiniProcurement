using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.InvoiceRequest;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers
{
    [Authorize(Roles = "USER_SUPPLY")]
    public class InvoiceController : ApplicationController
    {
        private readonly IInvoiceRequestService _invoiceRequestService;

        public InvoiceController(IInvoiceRequestService invoiceRequestService)
        {
            _invoiceRequestService = invoiceRequestService;
        }

        [HttpPost("create-invoice")]
        public async Task<IActionResult> CreateInvoice(CreateInvoiceDto createInvoiceDto)
        {
            await _invoiceRequestService.CreateInvoiceRequest(createInvoiceDto);
            return Ok();
        }

        [HttpPost("add-item-to-invoice/{id}")]
        public async Task<IActionResult> AddItemToInvoice([FromRoute] int id, CreateInvoiceItemDto createInvoiceItemDto)
        {
            await _invoiceRequestService.AddInvoiceItem(id, createInvoiceItemDto);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllInvoices()
        {
            var invoiceReuqests = await _invoiceRequestService.GetAllInvoiceRequests();
            return Ok(invoiceReuqests);
        }

        [HttpPost("process-invoice/{id}/fromPurchaseRequest/{prId}")]
        public async Task<IActionResult> ProcessInvoiceTransaction(int id, int prId)
        {
            await _invoiceRequestService.ProcessInvoiceTransaction(id, prId);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice([FromRoute] int id, [FromBody] UpdateInvoiceDto updateInvoiceDto)
        {
            await _invoiceRequestService.UpdateInvoice(id, updateInvoiceDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice([FromRoute] int id)
        {
            await _invoiceRequestService.DeleteInvoice(id);
            return NoContent();
        }

        [HttpPut("update-invoice/{id}/item/{itemId}")]
        public async Task<IActionResult> UpdateInvoiceItem([FromRoute] int id, [FromRoute] int itemId, [FromBody] UpdateInvoiceItemDto updateInvoiceItemDto)
        {
            await _invoiceRequestService.UpdateInvoiceItem(id, itemId, updateInvoiceItemDto);
            return Ok();
        }


        [HttpDelete("delete-invoice/{id}/item/{itemId}")]
        public async Task<IActionResult> DeleteInvoiceItem([FromRoute] int id, [FromRoute] int itemId)
        {
            await _invoiceRequestService.DeleteInvoiceItem(id, itemId);
            return NoContent();
        }
    }
}
