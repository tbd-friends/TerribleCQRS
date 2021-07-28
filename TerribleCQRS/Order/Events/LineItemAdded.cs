using System;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Order.Events
{
    public class LineItemAdded : IDomainEvent
    {
        public Guid ItemId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
