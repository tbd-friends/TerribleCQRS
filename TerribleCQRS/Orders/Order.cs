using System;
using System.Collections.Generic;
using System.Linq;
using TerribleCQRS.Core.Infrastructure;
using TerribleCQRS.Orders.Events;
using TerribleCQRS.Orders.ValueTypes;

namespace TerribleCQRS.Orders
{
    public class Order : AggregateRoot<OrderId>,
        IAccept<OrderCreated>,
        IAccept<LineItemAdded>,
        IAccept<LineItemRemoved>
    {
        public DateTime OrderDate { get; private set; }
        public string ReferenceNumber { get; private set; }
        public string CustomerName { get; private set; }
        public List<LineItem> LineItems { get; private set; }

        public decimal TotalValue => LineItems.Sum(i => i.Value);

        public Order(OrderId orderId)
            : base(orderId)
        {
            LineItems = new List<LineItem>();
        }

        public Order(OrderId orderId, string referenceNumber, DateTime orderDate, string customerName)
            : this(orderId)
        {
            RaiseEvent(new OrderCreated
            {
                Id = orderId,
                OrderDate = orderDate,
                ReferenceNumber = referenceNumber,
                CustomerName = customerName
            });
        }
        public void AddLineItem(Guid itemId, string description, decimal value)
        {
            RaiseEvent(new LineItemAdded
            {
                Id = Id,
                ItemId = itemId,
                Description = description,
                Value = value
            });
        }

        public void RemoveLineItem(Guid itemId)
        {
            RaiseEvent(new LineItemRemoved()
            {
                Id = Id,
                ItemId = itemId
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
