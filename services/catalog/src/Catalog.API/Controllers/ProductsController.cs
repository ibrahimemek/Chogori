using Catalog.API.Requests;
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
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateProductCommand(
                request.Name, request.Description, request.Amount, request.CurrencyCode, request.CategoryId);

            Guid result = await _sender.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = result }, result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var query = new GetAllProductsQuery(includeInactive);
            var result = await _sender.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet("category/{categoryId:guid}")]
        public async Task<IActionResult> GetByCategoryId([FromRoute] Guid categoryId, [FromQuery] bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var query = new GetProductsByCategoryQuery(categoryId, includeInactive);
            var results = await _sender.Send(query, cancellationToken);

            return Ok(results);
        }

        [HttpPut("{id:guid}/price")]
        public async Task<IActionResult> UpdatePrice([FromRoute] Guid id, [FromBody] UpdateProductPriceRequest request, CancellationToken cancellationToken)
        {
            var command = new UpdateProductPriceCommand(id, request.NewAmount, request.NewCurrencyCode);
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