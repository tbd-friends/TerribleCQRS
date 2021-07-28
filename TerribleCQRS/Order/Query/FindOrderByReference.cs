using MediatR;
using System;

namespace TerribleCQRS.Order.Query
{
    public class FindOrderByReference : IRequest<Guid>
    {
        public string Reference { get; set; }
    }
}
