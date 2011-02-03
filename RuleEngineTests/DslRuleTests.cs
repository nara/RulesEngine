using NUnit.Framework;
using RulesEngine;
using RulesEngine.BooEvaluator;

namespace RuleEngineTests
{
    [TestFixture]
    public class DslRuleTests
    {
        private BooLangEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            var dslEngineStorage = new DslEngineStorage();
            evaluator = new BooLangEvaluator(dslEngineStorage);
        }

        [Test]
        public void Can_Evaluate_Dsl_Expression()
        {
            var rule = "return (this.Price > 20 and this.Price < 15)";
            var order = new Order { Price = 10, Message = "Soemthing" };
            var result = evaluator.Evaluate(rule, order);
            Assert.IsTrue(result);
        }

        [Test]
        public void Can_Evaluate_Expression_As_ConditionalRule()
        {
            var statement = "return (this.Price > 20 or this.Price < 15)";
            var order = new Order { Price = 10, Message = "Soemthing" };
            EvaluatorAccessPoint.DslConditionEvaluator = evaluator;
            var condition = new DslCondition { DslStatement = statement };
            var rule = new ConditionalRule(condition);
            Assert.IsTrue(rule.Evaluate(order));
        }

        [Test]
        public void Can_Evaluate_Expression_As_ActionActivityRule()
        {
            var statement = "return (this.Price > 20 or this.Price < 15)";
            var order = new Order { Price = 10, Message = "Soemthing" };
            EvaluatorAccessPoint.DslConditionEvaluator = evaluator;
            var condition = new DslCondition { DslStatement = statement };
            var rule = new ActivityRule(condition, new ActionActivity<Order>(t =>
                                                                                 {
                                                                                     t.Message = "amp";
                                                                                 }));
            Assert.IsTrue(rule.Evaluate(order));
            Assert.AreEqual("amp", order.Message);
        }

        [Test]
        public void Can_Evaluate_Expression_With_DslActivity()
        {
            var statement = "return (this.Price > 20 or this.Price < 15)";
            var order = new Order { Price = 10, Message = "Soemthing" };
            EvaluatorAccessPoint.DslConditionEvaluator = evaluator;
            var condition = new DslCondition { DslStatement = statement };
            var rule = new ActivityRule(condition, new DslActivity
                                                       {
                                                           DslStatement = @"
this.Message = ""dsl"";
return true;"
                                                       });
            Assert.IsTrue(rule.Evaluate(order));
            Assert.AreEqual("dsl", order.Message);
        }
    }

    public class Order
    {
        public int Price;
        public string Message;
    }
}


  