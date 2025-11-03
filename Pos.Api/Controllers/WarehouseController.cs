using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Warehouse.Commands.CreateWarehouse;
using Pos.Application.Features.Warehouse.Commands.UpdateWarehouse;
using Pos.Application.Features.Warehouse.Queries.GetAllWarehouses;
using Pos.Application.Features.Warehouse.Queries.GetAllWarehousesSelect;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly ISender _sender;

        public WarehouseController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CrateAsync([FromBody]CreateWarehouseRequest createWarehouseRequest)
        {
            var response = await _sender.Send(createWarehouseRequest);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateWarehouseRequest updateWarehouseRequest)
        {
            var newRequest = updateWarehouseRequest with { Id = id };
            var response = await _sender.Send(newRequest);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _sender.Send(new GetAllWarehousesRequest());
            return Ok(response);
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetAllSelectAsync()
        {
            var response = await _sender.Send(new GetAllWarehousesSelectRequest());
            return Ok(response);
        }
    }
}
