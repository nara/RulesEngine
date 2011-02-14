using System;

namespace FluentFlow
{
    public class FalseDecisionNode<T> : FollowingNode<T>
    {
        public FollowingNode<T> WhenTrue(Action<FlowNode<T>> action)
        {
            action(new FlowNode<T>());
            return new FollowingNode<T>();
        }
    }
}