using Inventory.Domain.Entities;
using Inventory.Domain.Exceptions;
using Inventory.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Commands
{
    
    public record ReplenishStockCommand(Guid ProductId, int ReplenishedQuantity) : IRequest;


    public class ReplenishStockCommandHandler : IRequestHandler<ReplenishStockCommand>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReplenishStockCommandHandler(IStockItemRepository stockItemRepository, IUnitOfWork unitOfWork)
        {
            _stockItemRepository = stockItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReplenishStockCommand request, CancellationToken cancellationToken)
        {
            StockItem? stockItem = await _stockItemRepository.GetByProductIdAsync(request.ProductId, true, cancellationToken);
            if (stockItem == null)
                throw new KetNotFoundException(request.ProductId);
            stockItem.Replenish(request.ReplenishedQuantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }

        
    }
}
