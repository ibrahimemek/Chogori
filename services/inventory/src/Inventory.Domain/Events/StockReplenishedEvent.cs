using SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Events
{
    public record StockReplenishedEvent(Guid ProductId, int AddedQuantity, int NewTotalQuantity) : DomainEvent;
    
}
