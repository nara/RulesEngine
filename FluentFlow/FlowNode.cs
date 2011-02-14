using System;
using RulesEngine;

namespace FluentFlow
{
    public class FlowNode<T> : FlowElement<T>
    {
        public override void Evaluate(T instance)
        {
            throw new NotImplementedException();
        }

        public DecisionNode<T> Decide(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public DecisionNode<T> Decide<TRule>() where TRule : AbstractRule
        {
            throw new NotImplementedException();
        }

        public FollowingNode<T> Do(Action<FlowNode<T>> action)
        {
            throw new NotImplementedException();
        }

        public FollowingNode<T> Do(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}