using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using IronRuby;
using Microsoft.Scripting.Hosting;

namespace RulesEngine.IronRubyEvaluator
{
    public class RubyEngine
    {
        private readonly string dslFileName;
        private readonly List<Assembly> assemblies;
        private Type contextType;
        private string condition;
        private ScriptEngine engine;
        private ScriptSource source;

        public RubyEngine(Type type, string condition)
        {
            contextType = type;
            this.condition = condition;
            this.dslFileName = "RubyClasses\\RuleRuleFactory.rb";
            this.assemblies = new List<Assembly>
                                  {
                                      typeof(AbstractRule).Assembly,
                                      typeof(RubyEngine).Assembly,
                                      type.Assembly
                                  };
        }

        public ICondition Create()
        {
            this.engine = Ruby.CreateEngine();
            var rubyRuleTemplate = String.Empty;
            using(var stream = new StreamReader(this.dslFileName))
            {
                rubyRuleTemplate = stream.ReadToEnd();
            }
            rubyRuleTemplate = rubyRuleTemplate.Replace("$ruleAssembly$", this.contextType.Namespace.Replace(".", "::"));
            rubyRuleTemplate = rubyRuleTemplate.Replace("$contextType$", this.contextType.Name);
            rubyRuleTemplate = rubyRuleTemplate.Replace("$condition$", this.condition);
            this.source = engine.CreateScriptSourceFromString(rubyRuleTemplate);
            
            var scope = engine.CreateScope();
            assemblies.ForEach(a => engine.Runtime.LoadAssembly(a));

            engine.Execute(source.GetCode(), scope);
            var @class = engine.Runtime.Globals.GetVariable("RuleRuleFactory");

            var installer = engine.Operations.CreateInstance(@class);
            var rule = installer.Create();

            return (ICondition) rule;
        }
    }
}