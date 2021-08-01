using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TerribleCQRS.Core.Infrastructure;

namespace TerribleCQRS.Storage
{
    public class InMemoryEventStore : IEventStore
    {
        private Dictionary<string, List<IDomainEvent>> _store;
        private List<IDomainEventSubscriber> _subscribers;

        public InMemoryEventStore()
        {
            _store = new Dictionary<string, List<IDomainEvent>>();
            _subscribers = new List<IDomainEventSubscriber>();
        }

        public void AddSubscriber(IDomainEventSubscriber subscriber)
        {
            _subscribers.Add(subscriber);
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

                ProjectToSubscribers(@event, @event.GetType());
            }
        }

        private void ProjectToSubscribers(object @event, Type eventType)
        {
            var method = (from m in GetType().GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                          where m.IsGenericMethod && m.Name == "ProjectToSubscribers"
                          select m).SingleOrDefault();

            var invokeMethod = method.MakeGenericMethod(eventType);

            invokeMethod.Invoke(this, new object[] { @event });
        }

        private void ProjectToSubscribers<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            if (_subscribers.Any())
            {
                foreach (var subscriber in _subscribers)
                {
                    subscriber.Project(@event);
                }
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
    }
}
