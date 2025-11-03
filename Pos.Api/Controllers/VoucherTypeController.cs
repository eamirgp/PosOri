using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.VoucherType.Queries.GetAllVoucherTypesSelect;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoucherTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public VoucherTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetAllSelectAsync()
        {
            var response = await _sender.Send(new GetAllVoucherTypesSelectRequest());
            return Ok(response);
        }
    }
}
