using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.UnitOfMeasure.Queries.GetAllUnitOfMeasures;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitOfMeasureController : ControllerBase
    {
        private readonly ISender _sender;

        public UnitOfMeasureController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<UnitOfMeasureDto>>> GetAllAsync()
        {
            var resposne = await _sender.Send(new GetAllUnitOfMeasuresRequest());
            return Ok(resposne);
        }
    }
}
