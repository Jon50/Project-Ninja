using TongueTwister.Models;

namespace TongueTwister.Validation.Warnings
{
    public class ALCNameWarning : ValidationRule
    {
        /// <summary>
        /// Rule declaring that additional localization content items should have unique identifiers.
        /// </summary>
        public override string Code => "ALCI";
        
        public override string Name => "Additional \"Localized Content\" Identifier Warning";

        public override string Description => "When a localization has 2 or more additional \"localized content\" items, each item should have a unique identifier. Otherwise, it may become impossible to access items that share the same name.";

        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Warning;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            // todo: code cleanup potential here ... 
            
            if (model.Type != TongueTwisterModel.ModelType.Localization ||
                model.AdditionalLocalizedContent == null ||
                model.AdditionalLocalizedContent.Count == 0)
            {
                ruleViolation = null;
                return false;
            }

            var count = model.AdditionalLocalizedContent.Count;
            
            for (int startingIndex = 0; startingIndex < count; startingIndex++)
            {
                for (int currentIndex = startingIndex + 1; currentIndex < count; currentIndex++)
                {
                    if (model.AdditionalLocalizedContent[currentIndex].Identifier ==
                        model.AdditionalLocalizedContent[startingIndex].Identifier)
                    {
                        ruleViolation = new RuleViolation()
                        {
                            Code = Code,
                            Message = string.Format(StaticLabels.EditorLabels.Model.Warnings.AdditionalLocalizationContentWarning, model.AdditionalLocalizedContent[currentIndex].Identifier),
                            RuleViolationSeverity = ViolationLevel
                        };
                        return true;
                    }
                }
            }

            ruleViolation = null;
            return false;
        }
    }
}