using System;

namespace FluentFlow
{
    public class DicisionBranchNode<T> : FlowProcessNode<T>
    {
        protected ProcessNode<T> onOtherResult;

        public DicisionBranchNode(Action<FlowNode<T>> action, FlowElement<T> master)
            : base(action, master)
        {
        }

        public void EvaluateOtherResult(T instance)
        {
            if(onOtherResult != null) onOtherResult.Evaluate(instance);
        }
    }

    public class TrueFalseNode<T> : DicisionBranchNode<T>
    {
        public TrueFalseNode(Action<FlowNode<T>> action, FlowElement<T> master)
            : base(action, master)
        {
        }

        public ProcessNode<T> WhenFalse(Action<FlowNode<T>> action)
        {
            onOtherResult = new FlowProcessNode<T>(action, master);
            return onOtherResult;
        }
    }

    public class FalseTrueNode<T> : DicisionBranchNode<T>
    {
        public FalseTrueNode(Action<FlowNode<T>> action, FlowElement<T> master)
            : base(action, master)
        {
        }

        public ProcessNode<T> WhenTrue(Action<FlowNode<T>> action)
        {
            onOtherResult = new FlowProcessNode<T>(action, master);
            return onOtherResult;
        }
    }
}