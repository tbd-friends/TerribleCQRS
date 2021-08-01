using System;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Orders.Events
{
    public class LineItemRemoved : IDomainEvent
    {
        public Guid Id { get; set; }
    }
}