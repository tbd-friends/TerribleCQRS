using MediatR;
using System;

namespace TerribleCQRS.Orders.Query
{
    public class FindOrderByReference : IRequest<Guid>
    {
        public string Reference { get; set; }
    }
}
