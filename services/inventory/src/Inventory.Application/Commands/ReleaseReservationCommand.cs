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
    public record ReleaseReservationCommand(Guid ProductId, Guid OrderId, int ReleasedQuantity) : IRequest;


    public class ReleaseReservationCommandHandler : IRequestHandler<ReleaseReservationCommand>
    {
        private readonly IStockItemRepository _stockItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReleaseReservationCommandHandler(IStockItemRepository stockItemRepository, IUnitOfWork unitOfWork)
        {
            _stockItemRepository = stockItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(ReleaseReservationCommand request, CancellationToken cancellationToken)
        {
            StockItem? stockItem = await _stockItemRepository.GetByProductIdAsync(request.ProductId, true, cancellationToken);
            if (stockItem == null)
                throw new KetNotFoundException(request.ProductId);
            stockItem.ReleaseReservation(request.OrderId, request.ReleasedQuantity);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

        }
    }


}
