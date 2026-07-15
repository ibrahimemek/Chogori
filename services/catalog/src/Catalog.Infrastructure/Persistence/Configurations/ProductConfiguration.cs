using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CategoryId).IsRequired().HasMaxLength(36);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).HasMaxLength(1000);
            builder.Property(p => p.StockQuantity).IsRequired();
            builder.Property(p => p.IsActive).IsRequired();
            builder.Property(p => p.CreatedDate).IsRequired();

            builder.OwnsOne(p => p.Price, price =>
            {
                price.Property(m => m.Amount)
                .HasColumnName("PriceAmount")
                .HasPrecision(18, 2).IsRequired();

                price.Property(m => m.CurrencyCode)
                .HasColumnName("PriceCurrencyCode")
                .HasMaxLength(3).IsRequired();
            });

            builder.Ignore(p => p.DomainEvents);

            builder.HasIndex(p => p.CategoryId);
            builder.HasIndex(p => p.Name);


        }
    }
}
