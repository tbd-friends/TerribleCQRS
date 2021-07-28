using System;
using System.Collections.Generic;

namespace TerribleCQRS.Infrastructure
{
    public abstract class AggregateBase<TAggregateRoot> : IAggregate
        where TAggregateRoot : IAggregateRoot
    {
        public TAggregateRoot Id { get; }
        public List<IDomainEvent> UncommittedEvents { get; private set; }

        IAggregateRoot IAggregate.Id => Id;

        public AggregateBase(TAggregateRoot id)
        {
            Id = id;
            UncommittedEvents = new List<IDomainEvent>();
        }

        public void RaiseEvent<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            ((IAggregate)this).Apply(@event);

            UncommittedEvents.Add(@event);
        }

        void IAggregate.Apply(IDomainEvent @event)
        {
            ((dynamic)this).Apply((dynamic)@event);
        }
    }
}
