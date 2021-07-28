namespace TerribleCQRS.Infrastructure
{
    public interface IAggregateRoot
    {
        string ToString();
    }

    public abstract class AggregateRoot<TId> : IAggregateRoot
    {
        public TId Id { get; }

        public AggregateRoot(TId id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
