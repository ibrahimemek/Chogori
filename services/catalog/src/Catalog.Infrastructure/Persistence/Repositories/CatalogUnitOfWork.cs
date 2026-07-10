using Catalog.Domain.Repositories;
using SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.Repositories
{
    internal class CatalogUnitOfWork : IUnitOfWork
    {
        private readonly CatalogDbContext _context;

        public CatalogUnitOfWork(CatalogDbContext context)
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
