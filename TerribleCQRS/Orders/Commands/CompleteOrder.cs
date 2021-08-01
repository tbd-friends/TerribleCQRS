using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerribleCQRS.Orders.ValueTypes;

namespace TerribleCQRS.Orders.Commands
{
    public class CompleteOrder : IRequest
    {
        public OrderId Id { get; set; }
        public string PaymentReference { get; set; }
    }
}
