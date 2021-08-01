using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleCQRS.Core.Infrastructure
{
    public interface IEventStore
    {
        void AddSubscriber(IDomainEventSubscriber subscribe);
        void Save<TId>(AggregateRoot<TId> aggregate);
        void Load<TAggregate, TId>(TAggregate aggregate)
            where TAggregate : AggregateRoot<TId>;
    }
}
