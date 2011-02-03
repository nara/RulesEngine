namespace RulesEngine
{
    public abstract class AbstractActivity
    {
        public abstract void Execute(object context);
    }

    public abstract class AbstractActivity<T> : AbstractActivity
    {
        public abstract void Execute(T context);
    }
}