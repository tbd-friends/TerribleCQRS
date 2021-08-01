using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Order.Commands.Handlers
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
            var order = new OrderAggregate(request.OrderId);

            _store.Load<OrderAggregate, OrderId>(order);

            order.AddLineItem(Guid.NewGuid(), request.Description, request.Value);

            _store.Save(order);

            return Unit.Task;
        }
    }
}
