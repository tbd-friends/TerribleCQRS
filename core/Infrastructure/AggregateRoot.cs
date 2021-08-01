using System;
using System.Collections.Generic;

namespace TerribleCQRS.Core.Infrastructure
{
    public interface IAggregate
    {
        void Apply(IDomainEvent @event);
    }

    public abstract class AggregateRoot<TId> : IAggregate
    {
        public TId Id { get; protected set; }
        public List<IDomainEvent> UncommittedEvents { get; private set; }

        public AggregateRoot(TId id)
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
