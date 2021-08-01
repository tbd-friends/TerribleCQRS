using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Storage
{
    public class InMemoryEventStore : IEventStore
    {
        private Dictionary<string, List<IDomainEvent>> _store;

        public InMemoryEventStore()
        {
            _store = new Dictionary<string, List<IDomainEvent>>();
        }

        public void Save<TId>(AggregateRoot<TId> aggregate)
        {
            var aggregateExists = _store.ContainsKey(aggregate.Id.ToString());

            if (!aggregateExists)
            {
                _store.Add(aggregate.Id.ToString(), new List<IDomainEvent>());
            }

            foreach (var @event in aggregate.UncommittedEvents)
            {
                _store[aggregate.Id.ToString()].Add(@event);
            }
        }

        public void Load<TAggregate, TId>(TAggregate aggregate)
            where TAggregate : AggregateRoot<TId>
        {
            var aggregateExists = _store.ContainsKey(aggregate.Id.ToString());

            if (aggregateExists)
            {
                foreach (var @event in _store[aggregate.Id.ToString()])
                {
                    ((IAggregate)aggregate).Apply(@event);
                }
            }
        }

        public string FindAggregateRootByEvent(Func<IDomainEvent, bool> predicate)
        {
            foreach (var key in _store.Keys)
            {
                IDomainEvent result = _store[key].AsQueryable().FirstOrDefault(predicate);

                if (result != null)
                {
                    return key;
                }
            }

            return null;
        }
    }
}
