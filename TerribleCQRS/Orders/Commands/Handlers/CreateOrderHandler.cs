using MediatR;
using System.Threading;
using System.Threading.Tasks;
using TerribleCQRS.Core.Infrastructure;
using TerribleCQRS.Orders.ValueTypes;

namespace TerribleCQRS.Orders.Commands.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrder, OrderId>
    {
        private readonly IEventStore _store;

        public CreateOrderHandler(IEventStore store)
        {
            _store = store;
        }

        public Task<OrderId> Handle(CreateOrder request, CancellationToken cancellationToken)
        {
            var id = OrderId.New;

            var order = new Order(id,
                request.ReferenceNumber,
                request.OrderDate,
                request.CustomerName);

            _store.Save(order);

            return Task.FromResult(id);
        }
    }
}
