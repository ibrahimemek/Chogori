using Catalog.Domain.Repositories;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<CatalogDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("CatalogDatabase");
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(CatalogDbContext).Assembly.FullName));
            });

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUnitOfWork, CatalogUnitOfWork>();
           
            return services;
        }
    }
}
