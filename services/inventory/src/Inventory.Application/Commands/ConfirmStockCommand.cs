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
    public record ConfirmStockCommand(Guid ProductId, Guid OrderId, int ConfirmedQuantity) : IRequest;

    public class ConfirmStockCommandHandler : IRequestHandler<ConfirmStockCommand>
    {

        private readonly IStockItemRepository _stockItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ConfirmStockCommandHandler(IStockItemRepository stockItemRepository, IUnitOfWork unitOfWork)
        {
            _stockItemRepository = stockItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ConfirmStockCommand request, CancellationToken cancellationToken)
        {
            StockItem? item = await _stockItemRepository.GetByProductIdAsync(request.ProductId, true, cancellationToken);
            if (item == null) 
                throw new KetNotFoundException(request.ProductId);
            item.Confirm(request.OrderId, request.ConfirmedQuantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }



}
