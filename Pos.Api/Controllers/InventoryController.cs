using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Inventory.Queries.GetAllInventory;
using Pos.Application.Shared.Pagination;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ISender _sender;

        public InventoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams param, [FromQuery]Guid warehouseId)
        {
            var response = await _sender.Send(new GetAllInventoryRequest(param, warehouseId));
            return Ok(response);
        }
    }
}
