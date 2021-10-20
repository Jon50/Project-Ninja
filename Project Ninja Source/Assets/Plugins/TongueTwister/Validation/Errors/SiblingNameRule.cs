using System.Collections.Generic;
using TongueTwister.Extensions;
using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Validation.Errors
{
    /// <summary>
    /// Rule declaring that all models with the same parent should have unique names.
    /// </summary>
    public class SiblingNameRule : ValidationRule
    {
        public override string Code => "SNR";
        
        public override string Name => "Sibling Name Error";

        public override string Description => "Two or more models under the same parent should not have the same name. It is highly recommended to keep this enabled.";
        
        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Error;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            if (model.Parent == null)
            {
                ruleViolation = null;
                return false;
            }
            
            var siblings = model?.Parent?.Children ?? new List<TongueTwisterModel>();
            var sharedNameCount = 0;
            var thisFormattedName = model.FormattedName;
            
            foreach (var sibling in siblings)
            {
                if (sibling.Id == model.Id)
                {
                    continue;
                }
                
                if (sibling.FormattedName == thisFormattedName)
                {
                    sharedNameCount++;
                }
            }
            
            if (sharedNameCount > 0)
            {
                ruleViolation = new RuleViolation()
                {
                    Code = Code,
                    Message = 
                        string.Format(
                            EditorLabels.Model.Errors.SameNameAsSibling,
                            model.Type.GetName(),
                            sharedNameCount),
                    RuleViolationSeverity = RuleViolationSeverityType.Error
                };
                return true;
            }

            ruleViolation = null;
            return false;
        }
    }
}