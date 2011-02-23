RulesEngine - Define your business rules in boo DSL, Code in Flow Syntax
========================================================================

Rules engine allows you to define complex business rules in boo DSL as strings externally, outside of your source code, and execute them as part of your 
C# application. You can store rules in external text file / database, and change them without modifying your source code.

You can pass in any a POCO object as context to the rules and use it evaluate the rule. See example below.

Example:
-------

Lets define rules on an Order class.

	public class Order
    {
        public double TotalPrice;
        public double Discount;
    }
	
Your business rule can be if TotalPrice > 100, apply a discount of 10%. You can define both rule, and activity in boo DSL.

	var businessRule = "return (this.TotalPrice > 100)";
	var businessAction = @"this.Discount = 10; return true;";
	var condition = new DslCondition { DslStatement = statement };
	var rule = new ActivityRule(condition, new DslActivity
		{
			DslStatement = businessAction
		});
	rule.Evaluate(order);
	
In above example, you can define business rule and actions externally in a file or database ( to track what rules executed on orders ). You can simply 
create an ActivityRule and pass condition and activity. 

You can also create complex rules like:

	var complexRule = (rule1 & rule2) | ( (rule3 | rule4) & (rule5 | rule6) );
	complexRule.Evaluate(context);
	
Rules Engine is designed such that it can easily be extended to other DSL evaluators such as boo.

Rules Engine allows you to specify a Flow chart of your business requirement in easily understable syntax. The example below shows a sample flow.
You can also use DSL conditions and activities as part of the flow. (The flow is a just a sample. It is not be a real business requirement :) ).

	var flow = Flow.For<Order>()
                .Do(a => 
                    a.Price = 10)
                .Then
                .Decide(o => o.Price > 10)
                    .WhenTrue(a => a.Do(t => t.Price = 10)
                        .Then
                            .Decide(new DslCondition(condition))
                                .WhenTrue(b => b.Do(t => t.Price = 20))
                                .WhenFalse(b => b.Do(t => t.Price = 30))
                    )
                    .WhenFalse(a1 => a1.Do(t => t.Price = 30)
                        .Then.Do(new DslActivity(activity)).Then.Do(t => t.Price = 50).Then
                            .Decide(aa => aa.Price > 40).WhenFalse(a => a.Do(a2 => a2.Message ="test")));

	var order = new Order { Price = 16 };
    flow.Execute(order);
	
