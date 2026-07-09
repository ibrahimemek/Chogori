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
    public class GetAllProductsQuery : IRequest<IReadOnlyCollection<ProductDTO>>;

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyCollection<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IReadOnlyCollection<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<Product> products = await _productRepository.GetAllAsync(cancellationToken, false);

            return products.Select(product => product.ToDto()).ToList().AsReadOnly();
        }
    }


}
