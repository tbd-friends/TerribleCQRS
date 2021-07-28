using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Order.Commands.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrder>
    {
        private readonly IEventStore _store;

        public CreateOrderHandler(IEventStore store)
        {
            _store = store;
        }

        public Task<Unit> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var order = new OrderAggregate(Guid.NewGuid(),
                request.ReferenceNumber,
                request.OrderDate,
                request.CustomerName);

            _store.Save(order);

            return Unit.Task;
        }
    }
}
