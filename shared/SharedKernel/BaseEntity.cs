using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = new Guid();
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        private List<DomainEvent> _domainEvents { get; set; } = new List<DomainEvent>();
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        protected void AddDomainEvent (DomainEvent domainEvent)
        {
            _domainEvents.Add (domainEvent);
        }
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
    }
}
