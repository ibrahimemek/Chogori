using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.DTOs
{
    public record StockItemDTO(
        Guid Id,
        Guid ProductId,
        int QuantityOnHand,
        int QuantityReserved,
        int ReorderTreshold
        );

}
