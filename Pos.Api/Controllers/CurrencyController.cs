using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Currency.Queries.GetAllCurrenciesSelect;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly ISender _sender;

        public CurrencyController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetAllSelectAsync()
        {
            var response = await _sender.Send(new GetAllCurrenciesSelectRequest());
            return Ok(response);
        }
    }
}
