using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, bool tracking, CancellationToken ct, params Expression<Func<Product, object>>[] includes);
        Task<IReadOnlyList<Product>> GetAllAsync(bool tracking, CancellationToken cancellationToken, Expression<Func<Product, bool>>? filter = null, params Expression<Func<Product, object>>[] includes);
        Task<IReadOnlyList<Product>> GetByCategoryAsync(Guid categoryId, bool tracking, CancellationToken cancellationToken, bool includeInactive = false, params Expression<Func<Product, object>>[] includes);
        Task AddAsync(Product product, CancellationToken ct);
    }
}
