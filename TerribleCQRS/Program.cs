using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TerribleCQRS.Infrastructure;
using TerribleCQRS.Orders;
using TerribleCQRS.Orders.Commands;
using TerribleCQRS.Orders.ValueTypes;
using TerribleCQRS.Storage;

namespace TerribleCQRS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var provider = GetServiceProvider();

            var mediator = provider.GetService<IMediator>();
            var eventStore = provider.GetService<IEventStore>();

            var id = await mediator.Send(new CreateOrder { OrderDate = DateTime.Now, CustomerName = "Bob", ReferenceNumber = "order-1" });

            await mediator.Send(new AddLineItem { OrderId = id, Description = "Chicken", Value = 9.99M });
            await mediator.Send(new AddLineItem { OrderId = id, Description = "Waffles", Value = 13.99M });

            var order = new Order(id);

            eventStore.Load<Order, OrderId>(order);

            Console.WriteLine(order.TotalValue);
        }

        private static IServiceProvider GetServiceProvider()
        {
            var collection = new ServiceCollection();

            collection.AddMediatR(typeof(CreateOrder).Assembly);
            collection.AddScoped<IEventStore, InMemoryEventStore>();

            return collection.BuildServiceProvider();
        }
    }
}
