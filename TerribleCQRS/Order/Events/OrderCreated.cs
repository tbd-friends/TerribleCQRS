using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Order.Events
{
    public class OrderCreated : IDomainEvent
    {
        public DateTime OrderDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string CustomerName { get; set; }
    }
}
