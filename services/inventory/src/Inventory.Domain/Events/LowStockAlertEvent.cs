using SharedKernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Domain.Events
{
    public record LowStockAlertEvent(Guid ProductId, int CurrentQuantity, int Treshold) : DomainEvent;
}
