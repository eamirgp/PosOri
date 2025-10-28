using MediatR;
using Microsoft.AspNetCore.Mvc;
using Pos.Api.Models;
using Pos.Application.Features.Product.Commands.CreateProduct;
using Pos.Application.Features.Product.Commands.UpdateProduct;
using Pos.Application.Features.Product.Queries.GetAllProducts;
using Pos.Application.Shared.Pagination;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status409Conflict)]
        public async Task<ActionResult> CreateAsync([FromBody]CreateProductRequest createProductRequest)
        {
            var response = await _sender.Send(createProductRequest);
            return response.IsSuccess
                ? Created(string.Empty, new { productId = response.Value })
                : StatusCode(response.StatusCode, new ErrorResponse { Errors = response.Errors });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody]UpdateProductRequest updateProductRequest)
        {
            var newRequest = updateProductRequest with { Id = id };
            var response = await _sender.Send(newRequest);
            return response.IsSuccess
                ? NoContent()
                : StatusCode(response.StatusCode, new { errors = response.Errors });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllAsync([FromQuery]PaginationParams param)
        {
            var response = await _sender.Send(new GetAllProductsRequest(param));
            return Ok(response);
        }
    }
}
