using RulesEngine;

namespace FluentFlow
{
    public class ActivityProcessNode<T> : ProcessNode<T>
    {
        private AbstractActivity activity;
        public ActivityProcessNode(AbstractActivity activity, FlowElement<T> master)
            : base(master)
        {
            this.activity = activity;
        }

        internal override void Evaluate(T instance)
        {
            if (this.activity != null) activity.Execute(instance);
            base.Evaluate(instance);
        }
    }
}