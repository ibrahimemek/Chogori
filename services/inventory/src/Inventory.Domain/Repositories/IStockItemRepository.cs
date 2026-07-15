using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Repositories
{
    public interface IStockItemRepository
    {
        Task<StockItem?> GetByProductIdAsync(Guid productId, bool tracking, CancellationToken cancellationToken, params Expression<Func<StockItem, object>>[] includes);
        Task<IReadOnlyList<StockItem>> GetLowStockItemsAsync(bool tracking, CancellationToken cancellationToken, params Expression<Func<StockItem, object>>[] includes);
    }
}
