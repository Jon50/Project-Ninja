using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Validation.Errors
{
    /// <summary>
    /// Rule declaring that models have errors in if their children have errors.
    /// </summary>
    public class InheritChildErrorsRule : ValidationRule
    {
        public override string Code => "ICER";

        public override string Name => "Inherit Child Errors";

        public override string Description => "Models will receive errors if their children have errors. Disable if experiencing slowness or performance issues.";

        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Error;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            if (RecursivelyCheckChildren(model))
            {
                ruleViolation = new RuleViolation()
                {
                    Code = Code,
                    Message = EditorLabels.Model.Errors.ChildErrors,
                    RuleViolationSeverity = ViolationLevel
                };
                return true;
            }

            ruleViolation = null;
            return false;
        }

        private bool RecursivelyCheckChildren(TongueTwisterModel model)
        {
            if (!model.HasChildren) return false;

            foreach (var child in model.Children)
            {
                if (child.HasErrors || RecursivelyCheckChildren(child)) return true;
            }

            return false;
        }
    }
}