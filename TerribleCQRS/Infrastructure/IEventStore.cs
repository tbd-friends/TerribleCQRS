using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleCQRS.Infrastructure
{
    public interface IEventStore
    {
        void Save<TId>(AggregateRoot<TId> aggregate);
        void Load<TAggregate, TId>(TAggregate aggregate)
            where TAggregate : AggregateRoot<TId>;
        string FindAggregateRootByEvent(Func<IDomainEvent, bool> predicate); // Don't do this for real
    }
}
