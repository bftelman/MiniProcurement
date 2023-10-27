using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.PurchaseRequest;
using MiniProcurement.Data.Entities;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers;

[Authorize]
public class PurchaseRequestController : ApplicationController
{
    private readonly IPurchaseRequestService _purchaseRequestService;
    private readonly IConveyorService _conveyorService;
    
    public PurchaseRequestController(IPurchaseRequestService purchaseRequestService, IConveyorService conveyorService)
    {
        _purchaseRequestService = purchaseRequestService;
        _conveyorService = conveyorService;
    }

    /*[HttpPost("assign-approver/{id}/approver/{userId}")]
    public async Task<IActionResult> AssignApprover([FromRoute] int id, [FromRoute] int userId)
    {
        await _conveyorService.AssignApprover(id, userId);
        return Ok();
    }

    [HttpPost("approve-purchase/{id}")]
    public async Task<IActionResult> ApprovePurchase([FromRoute] int id)
    {
        await _conveyorService.ApproveDocument(id, User);
        return Ok();
    }*/

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

    [Authorize(Roles = "USER_DEMAND")]
    [HttpPost("{id}/add-purchase-request-item")]
    public async Task<IActionResult> AddPurchaseRequestItem([FromRoute] int id,
        [FromBody] CreatePurchaseRequestItemDto createPurchaseRequestItemDto)
    {
        await _purchaseRequestService.AddPurchaseRequestItem(id, createPurchaseRequestItemDto);
        return Ok();
    }

    [Authorize(Roles = "USER_DEMAND")]
    [HttpPost]
    public async Task<IActionResult> CreatePurchaseRequest([FromBody] CreatePurchaseRequestDto createPurchaseRequestDto)
    {
        await _purchaseRequestService.CreatePurchaseRequest(User, createPurchaseRequestDto);
        return Ok();
    }

    [Authorize(Roles = "USER_DEMAND")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePurchaseRequest([FromRoute] int id)
    {
        await _purchaseRequestService.DeletePurchaseRequest(id);
        return NoContent();
    }

    [Authorize(Roles = "USER_DEMAND")]

    [HttpPut("update-pr/{id}/item/{itemId}")]
    public async Task<IActionResult> UpdatePurchaseRequestItem([FromRoute] int id, [FromRoute] int itemId,
        [FromBody] UpdatePurchaseRequestItemDto updatePriDto)
    {
        await _purchaseRequestService.UpdatePurchaseRequestItem(id, itemId, updatePriDto);
        return Ok();
    }

    [Authorize(Roles = "USER_DEMAND")]

    [HttpDelete("delete-purchase/{id}/item/{itemId}")]
    public async Task<IActionResult> DeletePurchaseItem([FromRoute] int id, [FromRoute] int itemId)
    {
        await _purchaseRequestService.DeletePurchaseRequestItem(id, itemId);
        return NoContent();
    }
}