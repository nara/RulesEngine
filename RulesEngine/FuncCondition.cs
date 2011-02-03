using System;

namespace RulesEngine
{
    public class FuncCondition : AbstractCondition
    {
        private Func<object, bool> condition;
        public FuncCondition(Func<object, bool> condition)
        {
            this.condition = condition;
        }

        public override bool Evaluate<T>(T context)
        {
            return this.condition(context);
        }
    }
}