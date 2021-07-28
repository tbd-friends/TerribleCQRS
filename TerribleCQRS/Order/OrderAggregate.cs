using System;
using System.Collections.Generic;
using System.Linq;
using TerribleCQRS.Aggregates.ValueTypes;
using TerribleCQRS.Infrastructure;
using TerribleCQRS.Order.Events;

namespace TerribleCQRS.Order
{
    public class OrderAggregate : AggregateBase<OrderAggregateRoot>,
        IAccept<OrderCreated>,
        IAccept<LineItemAdded>,
        IAccept<LineItemRemoved>
    {
        public DateTime OrderDate { get; private set; }
        public string ReferenceNumber { get; private set; }
        public string CustomerName { get; private set; }
        public List<LineItem> LineItems { get; private set; }

        public decimal TotalValue => LineItems.Sum(i => i.Value);

        public OrderAggregate(Guid id)
            : base(new OrderAggregateRoot(id))
        {
            LineItems = new List<LineItem>();
        }

        public OrderAggregate(Guid id, string referenceNumber, DateTime orderDate, string customerName)
            : this(id)
        {
            RaiseEvent(new OrderCreated
            {
                OrderDate = orderDate,
                ReferenceNumber = referenceNumber,
                CustomerName = customerName
            });
        }
        public void AddLineItem(Guid itemId, string description, decimal value)
        {
            RaiseEvent(new LineItemAdded
            {
                ItemId = itemId,
                Description = description,
                Value = value
            });
        }

        public void RemoveLineItem(Guid itemId)
        {
            RaiseEvent(new LineItemRemoved()
            {
                Id = itemId
            });
        }

        public void Apply(OrderCreated @event)
        {
            OrderDate = @event.OrderDate;
            ReferenceNumber = @event.ReferenceNumber;
            CustomerName = @event.CustomerName;
        }

        public void Apply(LineItemAdded @event)
        {
            LineItems.Add(new LineItem
            {
                Id = @event.ItemId,
                Description = @event.Description,
                Value = @event.Value
            });
        }

        public void Apply(LineItemRemoved @event)
        {
            LineItems.Remove(LineItems.Single(i => i.Id == @event.Id));
        }
    }
}
