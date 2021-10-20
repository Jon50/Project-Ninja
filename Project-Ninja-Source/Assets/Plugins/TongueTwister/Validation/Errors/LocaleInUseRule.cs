using System.Linq;
using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Validation.Errors
{
    /// <summary>
    /// Rule declaring that localizations cannot share the same locale selection as the other locales in its parent.
    /// </summary>
    public class LocaleInUseRule : ValidationRule
    {
        public override string Code => "LIUR";
        
        public override string Name => "\"Locale In Use\" Error";

        public override string Description => "Display keys cannot have multiple localizations with the same locales. It is highly recommended to keep this enabled.";
        
        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Error;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            if (model.Type != TongueTwisterModel.ModelType.Localization || 
                model.Parent == null || 
                string.IsNullOrWhiteSpace(model.LocaleId))
            {
                ruleViolation = null;
                return false;
            }
            
            var siblings = model?.Parent?.Children
                .Where(child => child.Type == TongueTwisterModel.ModelType.Localization)
                .ToList();
            
            var sharedLocaleCount = 0;

            foreach (var sibling in siblings)
            {
                if (sibling.Id == model.Id)
                {
                    continue;
                }
                
                if (sibling.Type != TongueTwisterModel.ModelType.Localization)
                {
                    continue;
                }

                if (string.IsNullOrWhiteSpace(sibling.LocaleId) && sibling.LocaleId == model.LocaleId)
                {
                    sharedLocaleCount++;
                }
            }

            if (sharedLocaleCount > 0)
            {
                ruleViolation = new RuleViolation()
                {
                    Code = Code,
                    Message = 
                        string.Format(
                            EditorLabels.Model.Errors.LocaleAlreadyInUse,
                            sharedLocaleCount),
                    RuleViolationSeverity = RuleViolationSeverityType.Error
                };
                return true;
            }

            ruleViolation = null;
            return false;
        }
    }
}