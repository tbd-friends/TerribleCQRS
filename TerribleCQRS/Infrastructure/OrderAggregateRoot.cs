using System;

namespace TerribleCQRS.Infrastructure
{
    public class OrderAggregateRoot : AggregateRoot<Guid>
    {
        public OrderAggregateRoot(Guid guid) : base(guid)
        { }

        public override string ToString()
        {
            return $"orders/{Id}";
        }

        public static implicit operator OrderAggregateRoot(string id)
        {
            var components = id.Split("/");

            return new OrderAggregateRoot(Guid.Parse(components[1]));
        }
    }
}
