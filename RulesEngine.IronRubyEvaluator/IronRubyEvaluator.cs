using System.Collections.Generic;
using System.Reflection;

namespace RulesEngine.IronRubyEvaluator
{
    public class IronRubyEvaluator : IDslConditionEvaluator
    {
        public IronRubyEvaluator()
        {
        }

        public bool Evaluate<T>(string condition, T context)
        {
            var ruleEngine = new RubyEngine(context.GetType(), condition);
            var rule = ruleEngine.Create();
            return rule.Evaluate(context);
        }
    }
}