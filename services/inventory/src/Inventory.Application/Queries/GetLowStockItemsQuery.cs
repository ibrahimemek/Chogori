using Inventory.Application.DTOs;
using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Queries
{
    public record GetLowStockItemsQuery(bool IncludeInactive = false) : IRequest<IReadOnlyList<StockItemDTO>>;

    public class GetLowStockItemsQueryHandler : IRequestHandler<GetLowStockItemsQuery, IReadOnlyList<StockItemDTO>>
    {
        private readonly IStockItemRepository _stockItemRepository;

        public GetLowStockItemsQueryHandler(IStockItemRepository stockItemRepository)
        {
            _stockItemRepository = stockItemRepository;
        }

        public async Task<IReadOnlyList<StockItemDTO>> Handle(GetLowStockItemsQuery request, CancellationToken cancellationToken)
        {
            IReadOnlyList<StockItem> stockItems = await _stockItemRepository.GetLowStockItemsAsync(false, cancellationToken, request.IncludeInactive);
            if (stockItems == null) return null;
            return stockItems.Select(item => item.ToDto()).ToList().AsReadOnly();

        }
    }


}
