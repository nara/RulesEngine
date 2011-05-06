using System;

namespace FluentFlow
{
    public abstract class DecisionNode<T> : FlowElement<T>
    {
        private TrueFalseNode<T> trueNode;
        private FalseTrueNode<T> falseNode;

        protected DecisionNode(FlowElement<T> master) : base(master)
        {
        }

        public TrueFalseNode<T> WhenTrue(Action<FlowNode<T>> action)
        {
            trueNode = new TrueFalseNode<T>(action, master);
            return trueNode;
        }

        public FalseTrueNode<T> WhenFalse(Action<FlowNode<T>> action)
        {
            falseNode = new FalseTrueNode<T>(action, master);
            return falseNode;
        }

        protected abstract bool Condition(T instance);

        internal override void Evaluate(T instance)
        {
            var branchNode = trueNode ?? (falseNode ?? (DicisionBranchNode<T>)null);
            if (branchNode == null) return;

            if(this.Condition(instance)) 
                branchNode.Evaluate(instance);
            else 
                branchNode.EvaluateOtherResult(instance);
        }
    }
}