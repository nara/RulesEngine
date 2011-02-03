using System;

namespace RulesEngine
{
    internal sealed class Operation
    {
        public static bool And(bool rule1Result, bool rule2Result)
        {
            return rule1Result && rule2Result;
        }

        public static bool Or(bool rule1Result, bool rule2Result)
        {
            return rule1Result || rule2Result;
        }
    }
}