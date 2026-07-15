using Inventory.Domain.Repositories;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<InventoryDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("InventoryDatabase");
                options.UseNpgsql(connectionString, b => b.MigrationsAssembly(typeof(InventoryDbContext).Assembly.FullName));
            });

            services.AddScoped<IStockItemRepository, StockItemRepository>();
            services.AddScoped<IUnitOfWork, InventoryUnitOfWork>();

            return services;
        }
    }
}
