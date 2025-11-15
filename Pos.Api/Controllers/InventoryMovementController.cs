using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.InventoryMovement.Queries.GetAllInventoryMovements;
using Pos.Application.Shared.Pagination;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryMovementController : ControllerBase
    {
        private readonly ISender _sender;

        public InventoryMovementController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams param)
        {
            var response = await _sender.Send(new GetAllInventoryMovementsRequest(param));
            return Ok(response);
        }
    }
}
