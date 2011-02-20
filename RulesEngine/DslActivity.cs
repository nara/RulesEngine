namespace RulesEngine
{
    public class DslActivity : AbstractActivity
    {
        public string DslStatement { get; set; }

        public DslActivity(){}

        public DslActivity(string statement)
        {
            this.DslStatement = statement;
        }

        public override void Execute(object context)
        {
            if(EvaluatorAccessPoint.DslConditionEvaluator != null)
                EvaluatorAccessPoint.DslConditionEvaluator.Evaluate(DslStatement, context);
        }
    }
}