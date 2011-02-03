namespace RulesEngine
{
    public class ActivityRule : ConditionalRule
    {
        private AbstractActivity activity;

        public ActivityRule(AbstractCondition condition,
            AbstractActivity activity) : base(condition)
        {
            this.activity = activity;
        }

        public override bool Evaluate<T>(T context)
        {
            if(base.Evaluate<T>(context)) 
            {
                this.activity.Execute(context);
                return true;
            }
            return false;
        }
    }
}