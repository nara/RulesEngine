using System;

namespace FluentFlow
{
    public class TrueDecisionNode<T> : FollowingNode<T>
    {
        public FollowingNode<T> WhenFalse(Action<FlowNode<T>> action)
        {
            action(new FlowNode<T>());
            return new FollowingNode<T>();
        }
    }
}