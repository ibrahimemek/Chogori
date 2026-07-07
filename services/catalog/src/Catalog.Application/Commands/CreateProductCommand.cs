using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Catalog.Domain.ValueObjects;
using Catalog.Domain.Repositories;
using Catalog.Domain.Entities;

namespace Catalog.Application.Commands
{
    public record CreateProductCommand(
        string Name,
        string Description,
        decimal PriceAmount,
        string PriceCurrencyCode,
        Guid CategoryId) : IRequest<Guid>;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Money price = Money.Create(request.PriceAmount, request.PriceCurrencyCode);
            Product product = Product.Create(request.Name, request.Description, price, request.CategoryId);
            await _productRepository.AddAsync(product, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }


}
