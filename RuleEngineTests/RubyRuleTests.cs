using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using RulesEngine;
using RulesEngine.BooEvaluator;
using RulesEngine.IronRubyEvaluator;

namespace RuleEngineTests
{
    [TestFixture]
    public class RubyRuleTests
    {
        private IronRubyEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            evaluator = new IronRubyEvaluator();
        }

        [Test]
        public void Can_Evaluate_Ruby_Expression()
        {
            var rule = "this.Price > 20 or this.Price < 15";
            var order = new Order { Price = 10, Message = "Soemthing" };
            var result = evaluator.Evaluate(rule, order);
            Assert.IsTrue(result);
        }

        [Test]
        public void Can_Evaluate_Ruby_Expression_Again()
        {
            var rule = "this.Price > 20 and this.Price < 15";
            var order = new Order { Price = 10, Message = "Soemthing" };
            var result = evaluator.Evaluate(rule, order);
            Assert.IsFalse(result);
        }

        [Test]
        public void Can_Evaluate_Ruby_Expression_On_CardInfo_Context()
        {
            var rule = "this.IsVisa";
            var context = new CardInfo {IsVisa = true};
            var result = evaluator.Evaluate(rule, context);
            Assert.IsTrue(result);

            context = new CardInfo { IsVisa = false };
            result = evaluator.Evaluate(rule, context);
            Assert.IsFalse(result);
        }
    }

    public class CardInfo
    {
        public bool IsVisa;
    }
}


