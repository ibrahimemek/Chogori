using Catalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct);
        Task<IReadOnlyList<Product>> GetByCategoryAsync(Guid categoryId, CancellationToken ct);
        Task AddAsync(Product product, CancellationToken ct);
        void Update(Product product);
    }
}
