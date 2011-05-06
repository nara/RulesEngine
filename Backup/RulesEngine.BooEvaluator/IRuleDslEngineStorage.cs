using Rhino.DSL;

namespace RulesEngine.BooEvaluator
{
    public interface IRuleDslEngineStorage : IDslEngineStorage
    {
        string AddCondition(string condition);
    }
}