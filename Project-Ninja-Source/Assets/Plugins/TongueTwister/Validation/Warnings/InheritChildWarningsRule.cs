using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Validation.Warnings
{
    /// <summary>
    /// Rule declaring that child models cannot have warnings.
    /// </summary>
    public class InheritChildWarningsRule : ValidationRule
    {
        public override string Code => "ICWR";
        
        public override string Name => "Inherit Child Warnings";

        public override string Description => "Models will receive warnings if their children have warnings. Disable if experiencing slowness or performance issues.";
        
        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Warning;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            if (RecursivelyCheckChildren(model))
            {
                ruleViolation = new RuleViolation()
                {
                    Code = Code,
                    Message = EditorLabels.Model.Warnings.ChildWarnings,
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
                if (child.HasWarnings || RecursivelyCheckChildren(child)) return true;
            }

            return false;
        }
    }
}