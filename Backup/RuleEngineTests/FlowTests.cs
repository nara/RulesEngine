using System;
using FluentFlow;
using NUnit.Framework;
using RulesEngine;
using RulesEngine.BooEvaluator;

namespace RuleEngineTests
{
    [TestFixture]
    public class FlowTests
    {
        private BooLangEvaluator evaluator;

        [SetUp]
        public void Setup()
        {
            var dslEngineStorage = new DslEngineStorage();
            evaluator = new BooLangEvaluator(dslEngineStorage);
        }

        [Test]
        public void Can_Setup_Flow()
        {
            var flow = Flow.For<Order>()
                .Do(a => 
                    a.Price = 10)
                .Then
                .Decide(o => o.Price > 10)
                    .WhenTrue(a => a.Do(t => t.Price = 10)
                        .Then
                            .Decide(aa => aa.Price > 15)
                                .WhenTrue(b => b.Do(t => t.Price = 20))
                                .WhenFalse(b => b.Do(t => t.Price = 30))
                    )
                    .WhenFalse(a1 => a1.Do(t => t.Price = 30)
                        .Then.Do(t => t.Price = 40).Then.Do(t => t.Price = 50).Then
                            .Decide(aa => aa.Price > 40).WhenFalse(a => a.Do(a2 => a2.Message ="test")));

            var order = new Order {Price = 15};
            flow.Execute(order);
            Console.WriteLine(order.Price);
            Console.WriteLine(order.Message);
        }

        [Test]
        public void Can_Setup_Flow_With_Dsl_Conditions()
        {
            var statement = "return (this.Price > 15 and this.Price < 20)";
            EvaluatorAccessPoint.DslConditionEvaluator = evaluator;

            var flow = Flow.For<Order>()
                .Decide(new DslCondition(statement))
                .WhenTrue(a => a.Do(o => o.Message = "true"))
                .WhenFalse(a => a.Do(o => o.Message = "false"));

            var order = new Order { Price = 16 };
            flow.Execute(order);
            Assert.AreEqual("true", order.Message);
            order.Price = 21;
            flow.Execute(order);
            Assert.AreEqual("false", order.Message);
        }

        [Test]
        public void Can_Setup_Flow_With_Dsl_Conditions_And_DslActiviy()
        {
            var condition = "return (this.Price > 15 and this.Price < 20)";
            var activity = @"this.Message = ""true""; return true; ";
            EvaluatorAccessPoint.DslConditionEvaluator = evaluator;

            var flow = Flow.For<Order>()
                .Decide(new DslCondition(condition))
                .WhenTrue(a => a.Do(new DslActivity(activity)))
                .WhenFalse(a => a.Do(o => o.Message = "false"));

            var order = new Order { Price = 16 };
            flow.Execute(order);
            Assert.AreEqual("true", order.Message);
            order.Price = 21;
            flow.Execute(order);
            Assert.AreEqual("false", order.Message);
        }
    }
}