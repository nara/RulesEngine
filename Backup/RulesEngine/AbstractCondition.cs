namespace RulesEngine
{
    public abstract class AbstractCondition
    {
        public abstract bool Evaluate(object context);
    }

    public abstract class AbstractCondition<T> : AbstractCondition
    {
        public abstract bool Evaluate(T context);
    }
}