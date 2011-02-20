using System;

namespace FluentFlow
{
    public class FlowProcessNode<T> : ProcessNode<T>
    {
        private Action<FlowNode<T>> action;
        public FlowProcessNode(Action<FlowNode<T>> action, FlowElement<T> master)
            : base(master)
        {
            this.action = action;
        }

        internal override void Evaluate(T instance)
        {
            if (action != null)
            {
                var flowNode = new FlowNode<T>(master);
                action(flowNode);
                flowNode.Evaluate(instance);
            }
            base.Evaluate(instance);
        }
    }

}