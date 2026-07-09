using Catalog.Domain.Repositories;
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
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
