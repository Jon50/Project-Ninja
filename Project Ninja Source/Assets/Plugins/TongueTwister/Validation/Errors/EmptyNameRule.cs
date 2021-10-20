using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Validation.Errors
{
    /// <summary>
    /// Rule declaring that models cannot have empty names.
    /// </summary>
    public class EmptyNameRule : ValidationRule
    {
        public override string Code => "ENR";

        public override string Name => "Empty Model Name Error";

        public override string Description => "Models cannot have empty names.";

        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Error;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                ruleViolation = new RuleViolation()
                {
                    Code = Code,
                    Message = EditorLabels.Model.Errors.EmptyName,
                    RuleViolationSeverity = RuleViolationSeverityType.Error
                };

                return true;
            }
            
            ruleViolation = null;
            return false;
        }
    }
}