using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TerribleCQRS.Aggregates;
using TerribleCQRS.Infrastructure;
using TerribleCQRS.Order;
using TerribleCQRS.Order.Commands;
using TerribleCQRS.Order.Query;
using TerribleCQRS.Storage;

namespace TerribleCQRS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var provider = GetServiceProvider();

            var mediator = provider.GetService<IMediator>();

            await mediator.Send(new CreateOrder { OrderDate = DateTime.Now, CustomerName = "Bob", ReferenceNumber = "order-1" });

            var orderId = await mediator.Send(new FindOrderByReference { Reference = "order-1" });

            await mediator.Send(new AddLineItem { OrderId = orderId, Description = "Chicken", Value = 9.99M });
        }

        private static IServiceProvider GetServiceProvider()
        {
            var collection = new ServiceCollection();

            collection.AddMediatR(typeof(CreateOrder).Assembly);
            collection.AddScoped<IEventStore, InMemoryEventStore>();

            return collection.BuildServiceProvider();
        }

        private static void DemonstrateAggregate()
        {
            var idOfOrder = Guid.NewGuid();
            var eventStore = new InMemoryEventStore();

            var orderAggregate = new OrderAggregate(idOfOrder, "order-1", DateTime.Now, "Bob");

            orderAggregate.AddLineItem(Guid.NewGuid(), "Chicken", 9.99M);

            Console.WriteLine($"Total Value: {orderAggregate.TotalValue}");

            eventStore.Save(orderAggregate);

            var recoveredAggregate = new OrderAggregate(idOfOrder);

            eventStore.Load(recoveredAggregate);

            var waffles = Guid.NewGuid();

            recoveredAggregate.AddLineItem(waffles, "Waffles", 11.99M);

            Console.WriteLine($"Total Value: {recoveredAggregate.TotalValue}");

            recoveredAggregate.RemoveLineItem(waffles);

            eventStore.Save(recoveredAggregate);

            Console.WriteLine($"Total Value: {recoveredAggregate.TotalValue}");

        }
    }
}
