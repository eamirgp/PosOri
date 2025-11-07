using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Purchase.Commands.CreatePurchase;
using Pos.Application.Features.Purchase.Queries.GetAllPurchases;
using Pos.Application.Features.Purchase.Queries.GetPurchaseById;
using Pos.Application.Shared.Pagination;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ISender _sender;

        public PurchaseController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreatePurchaseRequest createPurchaseRequest)
        {
            var request = await _sender.Send(createPurchaseRequest);
            return request.IsSuccess
                ? Created(string.Empty, new { purchaseId = request.Value })
                : StatusCode(request.StatusCode, new { errors = request.Errors });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams param, [FromQuery]Guid? warehouseId)
        {
            var response = await _sender.Send(new GetAllPurchasesRequest(param, warehouseId));
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _sender.Send(new GetPurchaseByIdRequest(id));
            return response.IsSuccess
                ? Ok(response.Value)
                : StatusCode(response.StatusCode, new { errors = response.Errors });
        }
    }
}
