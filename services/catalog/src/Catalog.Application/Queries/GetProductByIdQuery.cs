using Catalog.Application.DTOs;
using Catalog.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public record GetProductByIdQuery(Guid ProductId) : IRequest<ProductDTO?>;

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO?>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null) return null;

            return new ProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.Price.Amount,
                product.Price.CurrencyCode,
                product.StockQuantity,
                product.CategoryId,
                product.IsActive);
        }
    }
}
