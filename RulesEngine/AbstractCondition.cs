namespace RulesEngine
{
    public abstract class AbstractCondition
    {
        public abstract bool Evaluate<T>(T context);
    }
}