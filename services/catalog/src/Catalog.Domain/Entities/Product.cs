using Catalog.Domain.Events;
using Catalog.Domain.Exceptions;
using Catalog.Domain.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Entities
{

    public class Product : AggregateRoot
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Money Price { get; private set; }
        public int StockQuantity { get; private set; }
        public Guid CategoryId { get; private set; }
        public bool IsActive { get; private set; }

        private Product() { }
        public static Product Create(string name, string description, Money price, Guid categoryId)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new DomainException("Product name cannot be empty");
            if (price == null) throw new DomainException("Product must have a price");

            Product product = new Product
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Name = name,
                Description = description,
                Price = price,
                StockQuantity = 0,
                CategoryId = categoryId,
                IsActive = true

            };

            product.RaiseDomainEvents(new ProductCreatedEvent(product.Id, product.Name, product.CategoryId));

            return product;
        }

        public void UpdatePrice(Money newPrice)
        {
            this.Price = newPrice;
            this.UpdatedDate = DateTime.UtcNow;

            this.RaiseDomainEvents(new ProductPriceUpdatedEvent(this.Id, this.Price));
        }

        public void Deactivate()
        {
            if (!IsActive) return;
            this.IsActive = false;
            this.RaiseDomainEvents(new ProductDeactivatedEvent(this.Id));
        }

        public void Activate()
        {
            if (IsActive) return;
            this.IsActive = true;
            this.RaiseDomainEvents(new ProductActivatedEvent(this.Id));
        }
    }
}
