using Catalog.Application.DTOs;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public record GetProductsByCategoryQuery(Guid CategoryId) : IRequest<IReadOnlyList<ProductDTO>>;
    public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, IReadOnlyList<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetProductsByCategoryQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyList<ProductDTO>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Product> products = await _productRepository.GetByCategoryAsync(request.CategoryId, cancellationToken, false);

            return products.Select(product => product.ToDto()).ToList().AsReadOnly();
        }
    }
}
