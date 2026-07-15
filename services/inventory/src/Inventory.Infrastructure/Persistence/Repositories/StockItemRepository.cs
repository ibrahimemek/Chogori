using Inventory.Domain.Entities;
using Inventory.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Persistence.Repositories
{
    internal class StockItemRepository : EfRepository<StockItem>, IStockItemRepository
    {
        public StockItemRepository(InventoryDbContext context) : base(context)
        {
        }

        public async Task<StockItem?> GetByProductIdAsync(Guid productId, bool tracking, CancellationToken cancellationToken, params Expression<Func<StockItem, object>>[] includes)
        {
            IQueryable<StockItem> query = _dbSet;

            if (!tracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(si => si.ProductId == productId, cancellationToken).ConfigureAwait(false);

        }

        public async Task<IReadOnlyList<StockItem>> GetLowStockItemsAsync(bool tracking, CancellationToken cancellationToken, params Expression<Func<StockItem, object>>[] includes)
        {
            IQueryable<StockItem> query = _dbSet;

            if (!tracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.Where(si => si.ReorderTreshold > si.QuantityOnHand).ToListAsync(cancellationToken).ConfigureAwait(false);

        }
    }
}
