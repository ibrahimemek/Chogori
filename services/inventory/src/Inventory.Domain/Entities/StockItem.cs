using Inventory.Domain.Events;
using Inventory.Domain.Exceptions;
using SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Entities
{

    public class StockItem : AggregateRoot
    {
        public Guid ProductId { get; private set; }
        public int QuantityOnHand { get; private set; }
        public int QuantityReserved { get; private set; }
        public int ReorderTreshold { get; private set; }


        public int AvailableQuantity => QuantityOnHand - QuantityReserved;

        public StockItem() { }

        public static StockItem Create(Guid productId, int initialQuantity, int reorderTreshold)
        {
            StockItem item = new StockItem
            {
                Id = new Guid(),
                ProductId = productId,
                QuantityOnHand = initialQuantity,
                QuantityReserved = 0,
                ReorderTreshold = reorderTreshold
            };

            return item;
        }

        public void Reserve(Guid orderId, int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Reservation quantity must be greater than zero.");

            if (quantity > AvailableQuantity)
                throw new InsufficentStockException($"Insufficient stock. Available: {AvailableQuantity}, Requested: {quantity}");

            this.QuantityReserved += quantity;

            this.RaiseDomainEvents(new StockReservedEvent(this.ProductId, orderId, quantity));

        }

        public void ReleaseReservation(Guid orderId, int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Release quantity must be greater than zero.");

            if (quantity > QuantityReserved)
                throw new DomainException("Cannot release more than currently reserved quantity.");

            this.QuantityReserved -= quantity;

            this.RaiseDomainEvents(new StockReservationReleasedEvent(this.ProductId, orderId, quantity));
        }

        public void Confirm(Guid orderId, int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Confirm quantity must be greater than zero.");

            if (quantity > QuantityReserved)
                throw new DomainException("Cannot confirm stock without prior reservation.");

            this.QuantityReserved -= quantity;
            this.QuantityOnHand -= quantity;

            this.RaiseDomainEvents(new StockConfirmedEvent(this.ProductId, orderId, quantity));

            if (this.QuantityOnHand < this.ReorderTreshold)
                this.RaiseDomainEvents(new LowStockAlertEvent(this.ProductId, this.QuantityOnHand, this.ReorderTreshold));
        }

        public void Replenish(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Replenishment quantity must be greater than zero.");

            this.QuantityOnHand += quantity;

            this.RaiseDomainEvents(new StockReplenishedEvent(this.ProductId, quantity, this.QuantityOnHand));
        }
    }
}
