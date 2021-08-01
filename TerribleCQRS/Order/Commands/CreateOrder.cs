﻿using MediatR;
using System;
using TerribleCQRS.Infrastructure;

namespace TerribleCQRS.Order.Commands
{
    public class CreateOrder : IRequest<OrderId>
    {
        public DateTime OrderDate { get; set; }
        public string ReferenceNumber { get; set; }
        public string CustomerName { get; set; }
    }
}
