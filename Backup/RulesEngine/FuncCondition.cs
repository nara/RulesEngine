using System;

namespace RulesEngine
{
    public class FuncCondition<T> : AbstractCondition<T>
    {
        private Func<T, bool> condition;
        public FuncCondition(Func<T, bool> condition)
        {
            this.condition = condition;
        }

        public override bool Evaluate(object context)
        {
            return this.condition((T)context);
        }

        public override bool Evaluate(T context)
        {
            return this.condition(context);
        }
    }
}