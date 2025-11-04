using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment;
using Pos.Application.Features.InventoryAdjustment.Queries.GetAllInventoryAdjustments;
using Pos.Application.Shared.Pagination;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryAdjustmentController : ControllerBase
    {
        private readonly ISender _sender;

        public InventoryAdjustmentController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateInventoryAdjustmentRequest createInventoryAdjustmentRequest)
        {
            var request = await _sender.Send(createInventoryAdjustmentRequest);
            return request.IsSuccess
                ? Created(string.Empty, new { adjustmentId = request.Value })
                : StatusCode(request.StatusCode, new { errors = request.Errors });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams param, [FromQuery] Guid? warehouseId)
        {
            var response = await _sender.Send(new GetAllInventoryAdjustmentsRequest(param, warehouseId));
            return Ok(response);
        }
    }
}
