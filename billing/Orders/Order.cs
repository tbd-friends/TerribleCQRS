using billing.Orders.ValueTypes;
using TerribleCQRS.Core.Infrastructure;

namespace billing.Orders
{
    public class Order : AggregateRoot<OrderId>
    {
        public bool IsPaid { get; set; }

        public Order(OrderId id) : base(id)
        { }       
    }
}
