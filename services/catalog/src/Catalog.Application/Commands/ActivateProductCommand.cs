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
    public record ActivateProductCommand(
        Guid ProductId) : IRequest;

    public class ActivateProductCommandHandler : IRequestHandler<ActivateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(ActivateProductCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _productRepository.GetByIdAsync(request.ProductId, true, cancellationToken);
            if (product == null)
                throw new ProductNotFoundException(request.ProductId);
            product.Activate();
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
