using System;

namespace FluentFlow
{
    public static class Flow
    {
        public static FlowNode<T> For<T>()
        {
            return new FlowNode<T>();
        }
    }
}