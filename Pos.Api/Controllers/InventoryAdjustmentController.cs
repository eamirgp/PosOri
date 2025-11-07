using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.InventoryAdjustment.Commands.CreateInventoryAdjustment;
using Pos.Application.Features.InventoryAdjustment.Queries.GetInventoryAdjustmentById;

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
        public async Task<IActionResult> CreateAsync(CreateInventoryAdjustmentRequest createInventoryAdjustmentRequest)
        {
            var response = await _sender.Send(createInventoryAdjustmentRequest);
            return response.IsSuccess
                ? Created(string.Empty, new { inventoryAdjustmentId = response.Value })
                : StatusCode(response.StatusCode, new { errors = response.Errors });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _sender.Send(new GetInventoryAdjustmentByIdRequest(id));
            return response.IsSuccess
                ? Ok(response.Value)
                : StatusCode(response.StatusCode, new { errors = response.Errors });
        }
    }
}
