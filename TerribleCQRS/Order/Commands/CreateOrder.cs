using MediatR;
using System;

namespace TerribleCQRS.Order.Commands
{
    public class CreateOrder : IRequest
    {
        public DateTime OrderDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string CustomerName { get; set; }
    }
}
