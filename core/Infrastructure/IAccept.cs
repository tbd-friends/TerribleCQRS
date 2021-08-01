namespace TerribleCQRS.Core.Infrastructure
{
    public interface IAccept<TEvent>
        where TEvent : class
    {
        void Apply(TEvent @event);
    }
}
