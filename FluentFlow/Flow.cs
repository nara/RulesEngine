using System;

namespace FluentFlow
{
    public class Flow<T> : FlowElement<T>
    {
        public override void Evaluate(T instance)
        {
            throw new NotImplementedException();
        }

        public static DecisionNode<T> Decide(Func<T, bool> func)
        {
            throw new NotImplementedException();
        }

        public static FollowingNode<T> Do(Action<FlowNode<T>> action)
        {
            throw new NotImplementedException();
        }

        public static FollowingNode<T> Do(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}