using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Application.Features.Product.Commands.CreateProduct;
using Pos.Application.Features.Product.Commands.UpdateProduct;
using Pos.Application.Features.Product.Queries.GetAllProducts;

namespace Pos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ISender _sender;

        public ProductController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateProductRequest createProductRequest)
        {
            var response = await _sender.Send(createProductRequest);
            return Created(string.Empty, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody]UpdateProductRequest updateProductRequest)
        {
            var newRequest = updateProductRequest with { Id = id };
            var response = await _sender.Send(newRequest);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _sender.Send(new GetAllProductsRequest());
            return Ok(response);
        }
    }
}
