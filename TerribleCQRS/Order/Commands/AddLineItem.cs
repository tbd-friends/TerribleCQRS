using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleCQRS.Order.Commands
{
    public class AddLineItem : IRequest
    {
        public Guid OrderId { get; set; } // We don't pass aggregate root around, so getting Id out again?
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
