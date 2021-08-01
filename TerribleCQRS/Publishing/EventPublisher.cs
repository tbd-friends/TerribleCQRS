using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TerribleCQRS.Core.Infrastructure;

namespace TerribleCQRS.Publishing
{
    public class EventPublisher : IDomainEventSubscriber
    {
        private IDictionary<string, string> _kafkaConfiguration;
        private IDictionary<string, string> _topics;

        public EventPublisher(IConfiguration configuration)
        {
            _kafkaConfiguration = new Dictionary<string, string>();
            _topics = new Dictionary<string, string>();

            configuration.GetSection("kafka").Bind("configuration:producer", _kafkaConfiguration);
            configuration.GetSection("kafka").Bind("topics", _topics);
        }

        public void Project<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            var producer = new ProducerBuilder<Guid, TEvent>(_kafkaConfiguration)
                .SetKeySerializer(new GuidSerializer())
                .SetValueSerializer(new JsonValueSerializer<TEvent>())
                .Build();

            string topicName = _topics[@event.GetType().Name];

            producer.Produce(topicName,
                new Message<Guid, TEvent> { Key = @event.Id, Value = @event });

            producer.Flush();
        }
    }

    public class KeySerializer : ISerializer<string>
    {
        public byte[] Serialize(string data, SerializationContext context)
        {
            return Encoding.UTF8.GetBytes(data);
        }
    }

    public class GuidSerializer : ISerializer<Guid>
    {
        public byte[] Serialize(Guid data, SerializationContext context)
        {
            return data.ToByteArray();
        }
    }

    public class JsonValueSerializer<T> : ISerializer<T>
    {
        public byte[] Serialize(T data, SerializationContext context)
        {
            return Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }));
        }
    }
}
