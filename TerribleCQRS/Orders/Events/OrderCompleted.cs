using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerribleCQRS.Core.Infrastructure;

namespace TerribleCQRS.Orders.Events
{
    public class OrderCompleted : IDomainEvent
    {
        public Guid Id {get; set; }
        public string ReferenceId { get; set; }
    }
}
