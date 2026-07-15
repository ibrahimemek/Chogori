using Inventory.Domain.Repositories;
using SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Persistence.Repositories
{
    internal class InventoryUnitOfWork : IUnitOfWork
    {
        private readonly InventoryDbContext _context;

        public InventoryUnitOfWork(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = await _context.SaveChangesAsync(cancellationToken);

            var aggregates = _context.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(e => e.Entity);


            foreach (var aggregate in aggregates)
            {
                aggregate.ClearDomainEvents();
            }


            return result;
        }
    }
}
