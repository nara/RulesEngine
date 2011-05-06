using System;
using Boo.Lang;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Ast;

namespace RulesEngine.BooEvaluator
{
    public abstract class DslModel
    {
        private Func<object, bool> evaluation;

        public DslModel()
        {}

        public abstract void Prepare();

        public void Initialize()
        {
            Prepare();
        }

        [Meta]
        public static Expression evaluate(BlockExpression formula)
        {
            var body = new Block(formula.LexicalInfo);
            for (int i = 0; i < formula.Body.Statements.Count; ++i)
            {
                var statement = formula.Body.Statements[i];

                if (statement is ExpressionStatement &&
                    i == formula.Body.Statements.Count - 1)
                {
                    var last = (ExpressionStatement)statement;
                    body.Statements.Add(new ReturnStatement(last.Expression));
                }
                else
                    body.Statements.Add(formula.Body.Statements[i]);
            }

            var result = new BlockExpression(body);
            result.Parameters.Add(new ParameterDeclaration("this",
                CompilerContext.Current.CodeBuilder
                    .CreateTypeReference(EvaluationContext.CurrentContext.GetType())));
            result.ReturnType = CompilerContext.Current.CodeBuilder
                .CreateTypeReference(typeof(bool));

            return new MethodInvocationExpression(
                new ReferenceExpression("SetEvaluationFunction"), result);
        }

        protected void SetEvaluationFunction(Func<object, bool> evaluation)
        {
            this.evaluation = evaluation;
        }

        public bool Evaluate()
        {
            return this.evaluation(EvaluationContext.CurrentContext);
        }
    }
}