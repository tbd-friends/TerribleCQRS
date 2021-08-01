using MediatR;
using TerribleCQRS.Infrastructure;
using TerribleCQRS.Orders.ValueTypes;

namespace TerribleCQRS.Orders.Commands
{
    public class AddLineItem : IRequest
    {
        public OrderId OrderId { get; set; } // We don't pass aggregate root around, so getting Id out again?
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
