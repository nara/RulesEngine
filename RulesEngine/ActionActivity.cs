using System;

namespace RulesEngine
{
    public class ActionActivity<T> : AbstractActivity<T>
    {
        private Action<T> action;

        public ActionActivity(Action<T> action)
        {
            this.action = action;
        }

        public override void Execute(object context)
        {
            this.action((T) context);
        }

        public override void Execute(T context)
        {
            this.action(context);
        }
    }
}