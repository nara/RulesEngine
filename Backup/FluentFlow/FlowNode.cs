using System;
using RulesEngine;

namespace FluentFlow
{
    public class FlowNode<T> : FlowElement<T>
    {
        private DecisionNode<T> decisionNode;
        private ProcessNode<T> actionNode;
        
        public FlowNode()
        {
        }

        public FlowNode(FlowElement<T> master): base(master)
        {
        }

        internal override void Evaluate(T instance)
        {
            if (decisionNode != null) decisionNode.Evaluate(instance);
            if (actionNode != null) actionNode.Evaluate(instance);
        }

        public DecisionNode<T> Decide(Func<T, bool> func)
        {
            decisionNode = new ConditionDecisionNode<T>(new FuncCondition<T>(func), master);
            return decisionNode;
        }

        public DecisionNode<T> Decide<TCondition>(TCondition condition) where TCondition : AbstractCondition
        {
            decisionNode = new ConditionDecisionNode<T>(condition, master);
            return decisionNode;
        }

        public ProcessNode<T> Do(Action<FlowNode<T>> action)
        {
            actionNode = new FlowProcessNode<T>(action, master);
            return actionNode;
        }

        public ProcessNode<T> Do(Action<T> action)
        {
            actionNode = new ActivityProcessNode<T>(new ActionActivity<T>(action), master);
            return actionNode;
        }

        public ProcessNode<T> Do(AbstractActivity activity)
        {
            actionNode = new ActivityProcessNode<T>(activity, master);
            return actionNode;
        }
    }
}