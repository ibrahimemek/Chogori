using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.DTOs
{
    public static class StockItemMapping
    {
        public static StockItemDTO ToDto(this StockItem stockItem) =>
            new(stockItem.Id, stockItem.ProductId, stockItem.QuantityOnHand, stockItem.QuantityReserved, stockItem.ReorderTreshold);
    }
}
