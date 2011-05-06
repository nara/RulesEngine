include RulesEngine
include RulesEngine::IronRubyEvaluator
include $ruleAssembly$

class RuleRuleFactory

	def Create()
		internalCreate $contextType$, do |this| $condition$ end
	end

	def internalCreate(type, &condition)
		RubyRule.of(type).new condition
	end
end

