using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleCQRS.Infrastructure
{
    public interface IEventStore
    {
        void Load<TAggregate>(TAggregate aggregate) where TAggregate : IAggregate;
        void Save<TId>(AggregateBase<TId> aggregate) where TId : IAggregateRoot;
        string FindAggregateRootByEvent(Func<IDomainEvent, bool> predicate); // Don't do this for real
    }
}
