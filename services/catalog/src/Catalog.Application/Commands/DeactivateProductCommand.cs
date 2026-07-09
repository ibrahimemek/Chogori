using Catalog.Domain.Entities;
using Catalog.Domain.Exceptions;
using Catalog.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Commands
{
    public record DeactivateProductCommand(
        Guid ProductId) : IRequest;

    public class DeactivateProductCommandHandler : IRequestHandler<DeactivateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _productRepository.GetByIdAsync(request.ProductId, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(request.ProductId);
            product.Deactivate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }


}
