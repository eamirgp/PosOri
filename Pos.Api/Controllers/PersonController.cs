using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Person.Commands.CreatePerson;
using Pos.Application.Features.Person.Commands.UpdatePerson;
using Pos.Application.Features.Person.Queries.GetAllPersons;
using Pos.Application.Shared.Pagination;

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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams param)
        {
            var response = await _sender.Send(new GetAllPersonsRequest(param));
            return Ok(response);
        }
    }
}
