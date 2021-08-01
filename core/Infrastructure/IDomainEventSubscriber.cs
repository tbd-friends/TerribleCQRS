using System;

namespace TerribleCQRS.Core.Infrastructure
{
    public interface IDomainEventSubscriber
    {
        void Project<TEvent>(TEvent @event) where TEvent : IDomainEvent;
    }
}
