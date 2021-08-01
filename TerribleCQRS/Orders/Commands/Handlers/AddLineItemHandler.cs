using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TerribleCQRS.Infrastructure;
using TerribleCQRS.Orders.ValueTypes;

namespace TerribleCQRS.Orders.Commands.Handlers
{
    public class AddLineItemHandler : IRequestHandler<AddLineItem>
    {
        private IEventStore _store;

        public AddLineItemHandler(IEventStore store)
        {
            _store = store;
        }

        public Task<Unit> Handle(AddLineItem request, CancellationToken cancellationToken)
        {
            var order = new Order(request.OrderId);

            _store.Load<Order, OrderId>(order);

            order.AddLineItem(Guid.NewGuid(), request.Description, request.Value);

            _store.Save(order);

            return Unit.Task;
        }
    }
}
