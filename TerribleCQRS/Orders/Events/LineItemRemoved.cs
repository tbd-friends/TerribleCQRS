using System;
using TerribleCQRS.Core.Infrastructure;

namespace TerribleCQRS.Orders.Events
{
    public class LineItemRemoved : IDomainEvent
    {
        public Guid Id { get; set; }
        public Guid ItemId { get; set; }
    }
}