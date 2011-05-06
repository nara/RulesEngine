namespace RulesEngine
{
    public class ConditionalRule : AbstractRule
    {
        private AbstractCondition condition;

        public ConditionalRule(AbstractCondition condition)
        {
            this.condition = condition;
        }

        public override bool Evaluate<T>(T context)
        {
            return condition.Evaluate(context);
        }
    }
}