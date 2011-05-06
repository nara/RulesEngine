using System;

namespace FluentFlow
{
    public class ProcessNode<T> : FlowElement<T>
    {
        private Action<FlowNode<T>> continuedAction;
        private Action<T> finalAction;
        private FlowNode<T> then;

        public ProcessNode(FlowElement<T> master) : base(master)
        {
        }

        public FlowNode<T> Then
        {
            get
            {
                if (then == null) then = new FlowNode<T>(master);
                return then;
            }
        }

        internal override void Evaluate(T instance)
        {
            if (then != null) then.Evaluate(instance);
        }
    }
}