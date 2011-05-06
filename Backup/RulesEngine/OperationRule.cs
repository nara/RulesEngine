using System;

namespace RulesEngine
{
    internal sealed class OperationRule : AbstractRule
    {
        private Func<bool, bool, bool> operation;
        private AbstractRule rule1;
        private AbstractRule rule2;

        public OperationRule(AbstractRule rule1, AbstractRule rule2, Func<bool, bool, bool> operation)
        {
            this.operation = operation;
            this.rule1 = rule1;
            this.rule2 = rule2;
        }

        public override bool Evaluate<T>(T context)
        {
            return operation(rule1.Evaluate(context), rule2.Evaluate(context));
        }
    }
}