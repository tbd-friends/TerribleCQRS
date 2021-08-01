using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using TerribleCQRS.Core.Infrastructure;
using TerribleCQRS.Orders;
using TerribleCQRS.Orders.Commands;
using TerribleCQRS.Orders.ValueTypes;
using TerribleCQRS.Publishing;
using TerribleCQRS.Storage;

namespace TerribleCQRS
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var provider = GetServiceProvider();

            var mediator = provider.GetService<IMediator>(); 

            var id = await mediator.Send(new CreateOrder { OrderDate = DateTime.Now, CustomerName = "Bob", ReferenceNumber = "order-1" });

            await mediator.Send(new AddLineItem { OrderId = id, Description = "Chicken", Value = 9.99M });
            await mediator.Send(new AddLineItem { OrderId = id, Description = "Waffles", Value = 13.99M });
        }

        private static IServiceProvider GetServiceProvider()
        {
            var collection = new ServiceCollection();

            var configuration = new ConfigurationBuilder()
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            collection.AddMediatR(typeof(CreateOrder).Assembly);
            collection.AddScoped<IConfiguration>(cfg => configuration);
            collection.AddScoped<EventPublisher>();
            collection.AddScoped<IEventStore, InMemoryEventStore>(provider =>
            {
                var result = new InMemoryEventStore();

                result.AddSubscriber(provider.GetService<EventPublisher>());

                return result;
            });

            return collection.BuildServiceProvider();
        }
    }
}
