using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Category.Commands.CreateCategory;
using Pos.Application.Features.Category.Commands.UpdateCategory;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ISender _sender;

        public CategoryController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCategoryRequest createCategoryRequest)
        {
            var response = await _sender.Send(createCategoryRequest);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCategoryRequest updateCategoryRequest)
        {
            var newRequest = updateCategoryRequest with { Id = id };
            var response = await _sender.Send(newRequest);
            return NoContent();
        }
    }
}
