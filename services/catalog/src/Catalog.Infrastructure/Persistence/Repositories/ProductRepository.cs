using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.Repositories
{
    internal class ProductRepository : EfRepository<Product>, IProductRepository
    {
        public ProductRepository(CatalogDbContext context) : base(context)
        {
        }


        public async Task<IReadOnlyList<Product>> GetByCategoryAsync(Guid categoryId, bool tracking, CancellationToken cancellationToken, bool includeInactive = false,  params Expression<Func<Product, object>>[] includes)
        {
            IQueryable<Product> query = _dbSet;

            if (!tracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return includeInactive ? await query.Where(p => p.CategoryId == categoryId).ToListAsync(cancellationToken).ConfigureAwait(false) :
                await query.Where(p => p.CategoryId == categoryId && p.IsActive).ToListAsync(cancellationToken).ConfigureAwait(false);
        }

        
    }
}
