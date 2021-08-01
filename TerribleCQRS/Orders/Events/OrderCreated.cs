using System;
using TerribleCQRS.Core.Infrastructure;

namespace TerribleCQRS.Orders.Events
{
    public class OrderCreated : IDomainEvent
    {
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string CustomerName { get; set; }
    }
}
