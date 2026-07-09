using Catalog.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.DTOs
{
    public record ProductDTO(
        Guid Id,
        string Name,
        string Description,
        decimal PriceAmount,
        string PriceCurrencyCode,
        int StockQuantity,
        Guid CategoryId,
        bool IsActive);
    
}
