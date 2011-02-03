using System;
using System.Collections.Generic;
using Boo.Lang.Compiler.IO;
using Rhino.DSL;

namespace RulesEngine.BooEvaluator
{
    public class DslEngineStorage : IRuleDslEngineStorage
    {
        private string condition;

        public string AddCondition(string condition)
        {
            this.condition = condition;
            return typeof(RuleDslModel).Name;
        }

        public Boo.Lang.Compiler.ICompilerInput CreateInput(string url)
        {
            return new StringInput(url, condition);
        }

        public string GetChecksumForUrls(System.Type dslEngineType, System.Collections.Generic.IEnumerable<string> urls)
        {
            return typeof(RuleDslModel).Name + "_" + dslEngineType.Name;
        }

        public string[] GetMatchingUrlsIn(string parentPath, ref string url)
        {
            return new [] { url };
        }

        public string GetTypeNameFromUrl(string url)
        {
            return typeof(RuleDslModel).Name;
        }

        public bool IsUrlIncludeIn(string[] urls, string parentPath, string url)
        {
            return Array.IndexOf(urls, url) >= 0;
        }

        public bool IsValidScriptUrl(string url)
        {
            return true;
        }

        public void NotifyOnChange(System.Collections.Generic.IEnumerable<string> urls, System.Action<string> action)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}