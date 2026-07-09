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
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct, bool tracking = true, params Expression<Func<Product, object>>[] includes);
        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken cancellationToken, bool tracking = true, params Expression<Func<Product, object>>[] includes);
        Task<IReadOnlyList<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken cancellationToken, bool tracking = true, params Expression<Func<Product, object>>[] includes);
        Task AddAsync(Product product, CancellationToken ct);
    }
}
