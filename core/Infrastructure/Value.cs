namespace TerribleCQRS.Core.Infrastructure
{
    public abstract class Value<T>
    {
        protected readonly T _value;

        protected Value(T value)
        {
            _value = value;
        }
    }
}
