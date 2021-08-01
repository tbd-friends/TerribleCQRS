using System;
using TerribleCQRS.Core.Infrastructure;

namespace billing.Orders.ValueTypes
{
    public class OrderId : Value<Guid>
    {
        public OrderId(Guid id) : base(id)
        { }
    }
}
