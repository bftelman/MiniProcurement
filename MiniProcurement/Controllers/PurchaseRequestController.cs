using Microsoft.AspNetCore.Mvc;
using MiniProcurement.Data.Contracts.Document;
using MiniProcurement.Services.Interfaces;

namespace MiniProcurement.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PurchaseRequestController: ControllerBase
    {
        private IPurchaseRequestDocumentService _purchaseRequestDocumentService;
        public PurchaseRequestController(IPurchaseRequestDocumentService purchaseRequestDocumentService)
        {
            _purchaseRequestDocumentService = purchaseRequestDocumentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPurchaseRequests()
        {
            return Ok(await _purchaseRequestDocumentService.GetPurchaseRequests());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseRequestById([FromRoute] int id)
        {
            return Ok(await _purchaseRequestDocumentService.GetPurchaseRequestDocumentById(id));
        }


        [HttpPost]
        public async Task<IActionResult> CreatePurchaseRequest([FromBody] CreatePurchaseRequestDto createPurchaseRequestDto)
        {
            await _purchaseRequestDocumentService.CreatePurchaseRequest(createPurchaseRequestDto);
            return Ok();
        }

    }
}
