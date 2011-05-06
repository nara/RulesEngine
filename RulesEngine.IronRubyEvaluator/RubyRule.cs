using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RulesEngine.IronRubyEvaluator
{
    public interface ICondition
    {
        bool Evaluate(object arg);
    }

    public class RubyRule<TContext> : ICondition
    {
        private readonly Predicate<object> evaluation;

        public RubyRule(Func<TContext, bool> evaluation)
        {
            this.evaluation = x => (x is Func<TContext>) ?
                                        evaluation.Invoke(((Func<TContext>)x).Invoke()) :
                                   (x is TContext) ?
                                        evaluation.Invoke((TContext)x) : false;
        }

        public bool Evaluate(object arg)
        {
            return evaluation(arg);
        }
    }
}
