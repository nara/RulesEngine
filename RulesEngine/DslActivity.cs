namespace RulesEngine
{
    public class DslActivity : AbstractActivity
    {
        public string DslStatement { get; set; }

        public override void Execute(object context)
        {
            if(EvaluatorAccessPoint.DslConditionEvaluator != null)
                EvaluatorAccessPoint.DslConditionEvaluator.Evaluate(DslStatement, context);
        }
    }
}