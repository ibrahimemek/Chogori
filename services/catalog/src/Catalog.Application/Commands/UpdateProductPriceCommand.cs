using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Repositories;
using Catalog.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands
{
    public record UpdateProductPriceCommand(
        Guid ProductId,
        decimal NewPriceAmount,
        string NewPriceCurrencyCode
        ) : IRequest;

    public class UpdateProductPriceCommandHandler : IRequestHandler<UpdateProductPriceCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateProductPriceCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(UpdateProductPriceCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null) 
                throw new ProductNotFoundException(request.ProductId);
            Money newPrice = Money.Create(request.NewPriceAmount, request.NewPriceCurrencyCode);
            product.UpdatePrice(newPrice);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            
        }
    }


}
