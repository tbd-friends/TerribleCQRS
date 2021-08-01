using System;
using TerribleCQRS.Core.Infrastructure;

namespace TerribleCQRS.Orders.Events
{
    public class LineItemAdded : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
