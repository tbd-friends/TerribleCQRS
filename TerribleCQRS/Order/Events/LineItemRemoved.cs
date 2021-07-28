using System;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Order.Events
{
    public class LineItemRemoved : IDomainEvent
    {
        public Guid Id { get; set; }
    }
}