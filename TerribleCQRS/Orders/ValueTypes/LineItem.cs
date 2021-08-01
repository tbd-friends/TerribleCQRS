using System;

namespace TerribleCQRS.Orders.ValueTypes
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
