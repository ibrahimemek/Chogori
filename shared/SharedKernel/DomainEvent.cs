using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public abstract record DomainEvent
    {
        public DateTime OcurredOn { get; init; } = DateTime.UtcNow;
    }
}
