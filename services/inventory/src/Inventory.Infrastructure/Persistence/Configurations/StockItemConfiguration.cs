using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Persistence.Configurations
{
    internal class StockItemConfiguration : IEntityTypeConfiguration<StockItem>
    {
        public void Configure(EntityTypeBuilder<StockItem> builder)
        {
            builder.ToTable("Stock Items");
            builder.HasKey(si => si.Id);

            builder.Property(si => si.QuantityReserved).IsRequired();
            builder.Property(si => si.QuantityOnHand).IsRequired();
            builder.Property(si => si.ReorderTreshold).IsRequired();
            builder.Property(si => si.CreatedDate).IsRequired();
            builder.Property(si => si.ProductId).IsRequired().HasMaxLength(36);

            builder.Ignore(si => si.DomainEvents);
            builder.HasIndex(si => si.ProductId);
        }
    }
}
