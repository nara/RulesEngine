namespace RulesEngine
{
    public class DslCondition : AbstractCondition
    {
        public string DslStatement { get; set; }

        public override bool Evaluate<T>(T context)
        {
            return EvaluatorAccessPoint.DslConditionEvaluator != null 
                && EvaluatorAccessPoint.DslConditionEvaluator.Evaluate(DslStatement, context);
        }
    }
}