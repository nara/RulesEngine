using RulesEngine;

namespace FluentFlow
{
    public class ConditionDecisionNode<T> : DecisionNode<T>
    {
        private AbstractCondition condition;

        public ConditionDecisionNode(AbstractCondition condition, FlowElement<T> master)
            : base(master)
        {
            this.condition = condition;
        }

        protected override bool Condition(T instance)
        {
            return condition.Evaluate(instance);
        }
    }
}