using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Sale.Commands.CreateSale;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISender _sender;

        public SaleController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateSaleRequest createSaleRequest)
        {
            var response = await _sender.Send(createSaleRequest);
            return response.IsSuccess
                ? Created(string.Empty, new { saleId = response.Value })
                : StatusCode(response.StatusCode, new { errors = response.Errors });
        }
    }
}
