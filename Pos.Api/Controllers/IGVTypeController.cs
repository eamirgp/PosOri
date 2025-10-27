using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.IGVType.Queries.GetAllIGVTypes;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IGVTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public IGVTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<ActionResult<List<IGVTypeDto>>> GetAllAync()
        {
            var response = await _sender.Send(new GetAllIGVTypesRequest());
            return Ok(response);
        }
    }
}
