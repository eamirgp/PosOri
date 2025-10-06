using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Person.Commands.CreatePerson;
using Pos.Application.Features.Person.Commands.UpdatePerson;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ISender _sender;

        public PersonController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreatePersonRequest createPersonRequest)
        {
            var response = await _sender.Send(createPersonRequest);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdatePersonRequest updatePersonRequest)
        {
            var newRequest = updatePersonRequest with { Id = id };
            var response = await _sender.Send(newRequest);
            return NoContent();
        }
    }
}
