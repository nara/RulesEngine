using System;

namespace FluentFlow
{
    public class DecisionNode<T> : FlowElement<T>
    {
        public DecisionNode(Func<T> condition)
        {
        }

        public TrueDecisionNode<T> WhenTrue(Action<FlowNode<T>> action)
        {
            throw new NotImplementedException();
        }

        public FalseDecisionNode<T> WhenFalse(Action<FlowNode<T>> action)
        {
            throw new NotImplementedException();
        }

        public override void Evaluate(T instance)
        {
            throw new NotImplementedException();
        }
    }
}