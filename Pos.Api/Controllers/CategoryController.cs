using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Category.Commands.CreateCategory;
using Pos.Application.Features.Category.Commands.UpdateCategory;
using Pos.Application.Features.Category.Queries.GetAllCategories;

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
        public async Task<IActionResult> CreateAsync([FromBody]CreateCategoryRequest createCategoryRequest)
        {
            var response = await _sender.Send(createCategoryRequest);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateCategoryRequest updateCategoryRequest)
        {
            var newRequest = updateCategoryRequest with { Id = id };
            var response = await _sender.Send(newRequest);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllAsync()
        {
            var response = await _sender.Send(new GetAllCategoriesRequest());
            return Ok(response);
        }
    }
}
