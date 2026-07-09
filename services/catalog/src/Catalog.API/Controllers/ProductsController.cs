using Catalog.Application.Commands;
using Catalog.Application.DTOs;
using Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;
        public ProductsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
        {
            Guid result = await _sender.Send(command, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            GetAllProductsQuery query = new GetAllProductsQuery();
            IReadOnlyCollection<ProductDTO> result = await _sender.Send(query, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            GetProductByIdQuery query = new GetProductByIdQuery(id);
            ProductDTO result = await _sender.Send(query, cancellationToken);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<IActionResult> GetByCategoryId([FromRoute] Guid categoryId, CancellationToken cancellationToken)
        {

            GetProductsByCategoryQuery query = new GetProductsByCategoryQuery(categoryId);
            IReadOnlyList<ProductDTO> results = await _sender.Send(query, cancellationToken);
            return Ok(results);
        }

        [HttpPut("{id:guid}/price")]
        public async Task<IActionResult> UpdatePrice([FromRoute] Guid id, [FromBody] UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductPriceCommand(id, request.NewPriceAmount, request.NewPriceCurrencyCode);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")] // soft delete
        public async Task<IActionResult> Deactivate([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new DeactivateProductCommand(id);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

        [HttpPut("{id:guid}/activate")]
        public async Task<IActionResult> Activate([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new ActivateProductCommand(id);
            await _sender.Send(command, cancellationToken);
            return NoContent();
        }

    }
}
