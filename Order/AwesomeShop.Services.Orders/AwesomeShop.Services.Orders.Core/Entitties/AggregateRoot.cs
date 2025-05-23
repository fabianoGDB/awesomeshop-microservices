using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeShop.Services.Orders.Core.Events;

namespace AwesomeShop.Services.Orders.Core.Entitties
{
    public class AggregateRoot : IEntityBase
    {
        private IList<IDomainEvent> _events = new List<IDomainEvent>();
        public Guid Id { get; protected set; }
        public IEnumerable<IDomainEvent> Events => _events;

        protected void AddEvent(IDomainEvent domainEvent)
        {
            if (_events == null)
                _events = new List<IDomainEvent>();

            _events.Add(domainEvent);
        }
    }
}