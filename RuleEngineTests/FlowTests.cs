using System;
using FluentFlow;
using NUnit.Framework;
using RulesEngine;

namespace RuleEngineTests
{
    [TestFixture]
    public class FlowTests
    {
        [Test]
        public void Can_Setup_Flow()
        {
            var flow = Flow<Order>
                .Do(a => a.Price = 10)
                .Then.Decide(o => o.Price > 10)
                .WhenTrue(a => 
                    a.Do(t => t.Price = 10)
                        .Then.Decide(aa => aa.Price > 15)
                            .WhenTrue(b => b.Do(t => t.Price = 20))
                            .WhenFalse(b => b.Do(t => t.Price = 30))
                )
                .WhenFalse(a1 => a1.Do(t => t.Price = 30).Then.Do(t => t.Price = 40).Then.Do(t => t.Price = 50).
                    Then.Decide(aa => aa.Price > 40).WhenFalse(a => a.Do(a2 => a2.Message ="test")));

            flow.Evaluate(new Order());
        }
    }
}