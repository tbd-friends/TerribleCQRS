namespace TerribleCQRS.Infrastructure
{
    public interface IAccept<TEvent>
        where TEvent : class
    {
        void Apply(TEvent @event);
    }
}
