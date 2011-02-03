using System;
using System.Collections.Generic;
using Rhino.DSL;

namespace RulesEngine.BooEvaluator
{
    public class BooLangEvaluator : IDslConditionEvaluator
    {
        private Dictionary<string, DslModel> parsedModels;
        private RuleDslEngine<RuleDslModel> ruleDslEngine;
        private DslFactory dslFactory;
        private IRuleDslEngineStorage dslEngineStorage;

        public BooLangEvaluator(IRuleDslEngineStorage dslEngineStorage)
        {
            parsedModels = new Dictionary<string, DslModel>();
            ruleDslEngine = new RuleDslEngine<RuleDslModel>();
            ruleDslEngine.Storage = dslEngineStorage;
            dslFactory = new DslFactory();
            dslFactory.Register<RuleDslModel>(ruleDslEngine);
            this.dslEngineStorage = dslEngineStorage;
        }

        public bool Evaluate<T>(string condition, T context)
        {
            EvaluationContext.CurrentContext = context;
            DslModel model = null;
            if (!parsedModels.ContainsKey(condition))
            {
                model = CreateRuleModelFor(condition);
                parsedModels.Add(condition, model);
            }
            else
            {
                model = parsedModels[condition];
            }
            return model.Evaluate();
        }

        private DslModel CreateRuleModelFor(string condition)
        {
            DslModel model = null;
            lock(dslFactory)
            {
                var wrapperRule = String.Format(@"
evaluate:
    {0}
", condition);
                var url = dslEngineStorage.AddCondition(wrapperRule);
                model = dslFactory.TryCreate<RuleDslModel>(url);
                model.Initialize();
            }
            return model;
        }
    }
}