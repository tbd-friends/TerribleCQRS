using System;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Orders.ValueTypes
{
    public class OrderId : Value<Guid>
    {
        public OrderId(Guid guid) : base(guid)
        { }

        public override string ToString()
        {
            return $"order/{_value}";
        }

        public static implicit operator OrderId(string id)
        {
            var components = id.Split("/");

            return new OrderId(Guid.Parse(components[1]));
        }

        public static OrderId New => new OrderId(Guid.NewGuid());
    }
}
