using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerribleCQRS.Core.Infrastructure;
using TerribleCQRS.Orders.ValueTypes;

namespace TerribleCQRS.Orders.Commands.Handlers
{
    public class CompleteOrderHandler : IRequestHandler<CompleteOrder>
    {
        private readonly IEventStore _store;

        public CompleteOrderHandler(IEventStore store)
        {
            _store = store;
        }

        public Task<Unit> Handle(CompleteOrder request, CancellationToken cancellationToken)
        {
            var order = new Order(request.Id);

            _store.Load<Order, OrderId>(order);

            order.ApplyPayment(request.PaymentReference);

            _store.Save(order);

            return Unit.Task;
        }
    }
}
