using System;

namespace RulesEngine
{
    public abstract class AbstractRule
    {
        public abstract bool Evaluate<T>(T context);
        public string Name { get; set; }


        public static AbstractRule operator &(AbstractRule rule1, AbstractRule rule2)
        {
            return new OperationRule(rule1, rule2, Operation.And);
        }

        public static AbstractRule operator |(AbstractRule rule1, AbstractRule rule2)
        {
            return new OperationRule(rule1, rule2, Operation.Or);
        }
    }
}