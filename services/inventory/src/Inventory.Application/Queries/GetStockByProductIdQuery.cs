using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using Inventory.Domain.Exceptions;
using Inventory.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Queries
{
    public record GetStockByProductIdQuery(Guid ProductId) : IRequest<StockItemDTO?>;

    public class GetStockByProductIdQueryHandler : IRequestHandler<GetStockByProductIdQuery, StockItemDTO?>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetStockByProductIdQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<StockItemDTO> Handle(GetStockByProductIdQuery request, CancellationToken cancellationToken)
        {
            StockItem? stockItem = await _stockItemRepository.GetByProductIdAsync(request.ProductId, false, cancellationToken);
            if (stockItem == null) return null;
            return stockItem.ToDto();
        }
    }


}
