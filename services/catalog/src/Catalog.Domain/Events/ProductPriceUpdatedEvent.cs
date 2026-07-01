using Catalog.Domain.ValueObjects;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Events
{
    public record ProductPriceUpdatedEvent(Guid ProductId, Money NewPrice) : DomainEvent;
    
}
