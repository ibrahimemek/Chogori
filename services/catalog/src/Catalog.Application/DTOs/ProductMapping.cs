using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.DTOs
{
    public static class ProductMapping
    {
        public static ProductDTO ToDto(this Product product) =>
            new(product.Id, product.Name, product.Description,
                product.Price.Amount, product.Price.CurrencyCode,
                product.StockQuantity, product.CategoryId, product.IsActive);
    }
}
