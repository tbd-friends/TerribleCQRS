using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerribleCQRS.Aggregates.ValueTypes
{
    public class LineItem
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
    }
}
