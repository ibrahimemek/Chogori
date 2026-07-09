using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.Repositories
{
    internal class EfRespository<T> where T : BaseEntity
    {
        protected readonly CatalogDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EfRespository(CatalogDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (!tracking) 
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return await query.FirstOrDefaultAsync(e => e.Id == id, cancellationToken).ConfigureAwait(false);
            
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken, bool tracking = true, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;
            if (!tracking)
                query = query.AsNoTracking();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return await query.ToListAsync(cancellationToken);

        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken).ConfigureAwait(false);
        }

       
    }
}
