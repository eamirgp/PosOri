using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.DocumentType.Queries.GetAllDocumentTypesSelect;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentTypeController : ControllerBase
    {
        private readonly ISender _sender;

        public DocumentTypeController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetAllSelect()
        {
            var response = await _sender.Send(new GetAllDocumentTypesSelectRequest());
            return Ok(response);
        }
    }
}
