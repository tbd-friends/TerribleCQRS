using System;

namespace TerribleCQRS.Core.Infrastructure
{
    public interface IDomainEvent
    {
        public Guid Id { get; set; }
    }
}
