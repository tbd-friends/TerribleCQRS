using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TerribleCQRS.Infrastructure;
using TerribleCQRS.Orders.Events;

namespace TerribleCQRS.Orders.Query.Handlers
{
    public class FindOrderByReferenceHandler : IRequestHandler<FindOrderByReference, Guid>
    {
        private IEventStore _store;

        public FindOrderByReferenceHandler(IEventStore store)
        {
            _store = store;
        }

        public Task<Guid> Handle(FindOrderByReference request, CancellationToken cancellationToken)
        {
            var orderByReference = _store.FindAggregateRootByEvent((@event) =>
           {
               if (@event is OrderCreated order)
               {
                   return order.ReferenceNumber == request.Reference;
               }

               return false;
           });

            return Task.FromResult(Guid.NewGuid());
        }
    }
}
