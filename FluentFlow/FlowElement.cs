namespace FluentFlow
{
    public abstract class FlowElement<T>
    {
        public abstract void Evaluate(T instance);
    }
}