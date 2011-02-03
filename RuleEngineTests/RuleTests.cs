using NUnit.Framework;
using RulesEngine;

namespace RuleEngineTests
{
    [TestFixture]
    public class RuleTests
    {
        [Test]
        public void Can_Add_Rules()
        {
            var rule1 = new ConditionalRule(new FuncCondition(t => true));
            var rule2 = new ConditionalRule(new FuncCondition(t => false));

            var rule = rule1 & rule2;
            Assert.IsFalse(rule.Evaluate(10));
        }

        [Test]
        public void Can_Or_Rules()
        {
            var rule1 = new ConditionalRule(new FuncCondition(t => true));
            var rule2 = new ConditionalRule(new FuncCondition(t => false));

            var rule = rule1 | rule2;
            Assert.IsTrue(rule.Evaluate(10));
        }

        [Test]
        public void Can_Do_Complex_Rule()
        {
            var rule1 = new ConditionalRule(new FuncCondition(t => true)){ Name = "rule1" };
            var rule2 = new ConditionalRule(new FuncCondition(t => false)) { Name = "rule2" };
            var rule3 = new ConditionalRule(new FuncCondition(t => true)) { Name = "rule3" };
            var rule4 = new ConditionalRule(new FuncCondition(t => false)) { Name = "rule4" };
            var rule5 = new ConditionalRule(new FuncCondition(t => true)) { Name = "rule5" };
            var rule6 = new ConditionalRule(new FuncCondition(t => false)) { Name = "rule6" };

            var rule = (rule1 & rule2) | ( (rule3 | rule4) & (rule5 | rule6) );
            Assert.IsTrue(rule.Evaluate(10));
        }
    }
}