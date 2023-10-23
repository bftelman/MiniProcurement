using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.PurchaseRequest;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers
{
    public class PurchaseRequestController : ApplicationController
    {
        private IPurchaseRequestService _purchaseRequestService;
        public PurchaseRequestController(IPurchaseRequestService purchaseRequestService)
        {
            _purchaseRequestService = purchaseRequestService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchaseRequests()
        {
            return Ok(await _purchaseRequestService.GetPurchaseRequests());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseRequestById([FromRoute] int id)
        {
            return Ok(await _purchaseRequestService.GetPurchaseRequestById(id));
        }

        [HttpGet("{id}/get-purchase-items")]
        public async Task<IActionResult> GetPurchaseRequestItems([FromRoute] int id)
        {
            return Ok(await _purchaseRequestService.GetPurchaseRequestItems(id));
        }

        [HttpGet("{id}/get-purchase-item-by-id/{itemId}")]
        public async Task<IActionResult> GetPurchaseRequestItem([FromRoute] int id, [FromRoute] int itemId)
        {
            return Ok(await _purchaseRequestService.GetPurchaseRequestItem(id, itemId));
        }

        [HttpPost("{id}/add-purchase-request-item")]
        public async Task<IActionResult> AddPurchaseRequestItem([FromRoute] int id, [FromBody] CreatePurchaseRequestItemDto createPurchaseRequestItemDto)
        {
            await _purchaseRequestService.AddPurchaseRequestItem(id, createPurchaseRequestItemDto);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> CreatePurchaseRequest([FromBody] CreatePurchaseRequestDto createPurchaseRequestDto)
        {
            await _purchaseRequestService.CreatePurchaseRequest(createPurchaseRequestDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseRequest([FromRoute] int id)
        {
            await _purchaseRequestService.DeletePurchaseRequest(id);
            return NoContent();
        }

        [HttpPut("update-pr/{id}/item/{itemId}")]
        public async Task<IActionResult> UpdatePurchaseRequestItem([FromRoute] int id, [FromRoute] int itemId, [FromBody] UpdatePurchaseRequestItemDto updatePriDto)
        {
            await _purchaseRequestService.UpdatePurchaseRequestItem(id, itemId, updatePriDto);
            return Ok();
        }


        [HttpDelete("delete-purchase/{id}/item/{itemId}")]
        public async Task<IActionResult> DeletePurchaseItem([FromRoute] int id, [FromRoute] int itemId)
        {
            await _purchaseRequestService.DeletePurchaseRequestItem(id, itemId);
            return NoContent();
        }

    }
}
