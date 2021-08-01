using System;

namespace TerribleCQRS.Infrastructure
{
    public class OrderId : Value<Guid>
    {
        private readonly Guid _id;

        public OrderId(Guid guid)
        {
            _id = guid;
        }

        public override string ToString()
        {
            return $"order/{_id}";
        }

        public static implicit operator OrderId(string id)
        {
            var components = id.Split("/");

            return new OrderId(Guid.Parse(components[1]));
        }

        public static OrderId New => new OrderId(Guid.NewGuid());
    }
}
