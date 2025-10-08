using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Purchase.Commands.CreatePurchase;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly ISender _sender;

        public PurchaseController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreatePurchaseRequest createPurchaseRequest)
        {
            var request = await _sender.Send(createPurchaseRequest);
            return request.IsSuccess
                ? Created(string.Empty, new { purchaseId = request.Value })
                : StatusCode(request.StatusCode, new { errors = request.Errors });
        }
    }
}
