using Catalog.Domain.Entities;
using Catalog.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Catalog.Domain.Events;
using Catalog.Domain.Exceptions;

namespace Catalog.UnitTests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Create_ShouldRaise_ProductCreatedEvent()
        {
            string name = "laptop";
            string description = "technology";
            Money price = Money.Create(50000, "TL");
            Guid categoryId = Guid.NewGuid();
            Product product = Product.Create(name, description, price, categoryId);

            product.DomainEvents.Should().ContainSingle().Which.Should().BeOfType<ProductCreatedEvent>();

            ProductCreatedEvent domainEvent = (ProductCreatedEvent) product.DomainEvents.First();
            domainEvent.ProductId.Should().Be(product.Id);
        }

        [Fact]
        public void Product_Create_WithNegativePrice_ShouldThrow_DomainException()
        {
            string name = "laptop";
            string description = "technology";
            Guid categoryId = Guid.NewGuid();
            Action act = () => Product.Create(name, description, Money.Create(-5, "TL"), categoryId);

            act.Should().Throw<DomainException>();
        }

        [Fact]
        public void Product_UpdatePrice_ShouldRaise_ProductPriceUpdatedEvent()
        {
            string name = "laptop";
            string description = "technology";
            Money price = Money.Create(50000, "TL");
            Guid categoryId = Guid.NewGuid();
            Product product = Product.Create(name, description, price, categoryId);

            product.ClearDomainEvents();

            Money newPrice = Money.Create(500, "USD");
            product.UpdatePrice(newPrice);

            product.DomainEvents.Should().ContainSingle().Which.Should().BeOfType<ProductPriceUpdatedEvent>();

            ProductPriceUpdatedEvent domainEvent = (ProductPriceUpdatedEvent)product.DomainEvents.First();
            domainEvent.ProductId.Should().Be(product.Id);

        }

        [Fact]
        public void Product_Deactivate_ShouldSetIsActive_False()
        {
            string name = "laptop";
            string description = "technology";
            Money price = Money.Create(50000, "TL");
            Guid categoryId = Guid.NewGuid();
            Product product = Product.Create(name, description, price, categoryId);

            product.IsActive.Should().BeTrue();
            product.Deactivate();
            product.IsActive.Should().BeFalse();
        }

        [Fact]
        public void Product_Activate_ShouldSetIsActive_True()
        {
            string name = "laptop";
            string description = "technology";
            Money price = Money.Create(50000, "TL");
            Guid categoryId = Guid.NewGuid();
            Product product = Product.Create(name, description, price, categoryId);

            product.IsActive.Should().BeTrue();
            product.Deactivate();
            product.IsActive.Should().BeFalse();

            product.Activate();
            product.IsActive.Should().BeTrue();
        }
    }
}
