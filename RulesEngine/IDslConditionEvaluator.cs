namespace RulesEngine
{
    public interface IDslConditionEvaluator
    {
        bool Evaluate<T>(string condition, T context);
    }
}