using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Validation.Warnings
{
    /// <summary>
    /// Rule declaring that all group and display key models should have compiler friendly names starting with a letter.
    /// </summary>
    public class CompilerNameDoesNotStartWithLetterWarning : ValidationRule
    {
        private const string NUMBER_CHARACTERS = "0123456789";

        public override string Code => "CNFL";
        
        public override string Name => "C# Compiler Name Warning - \"Name Does Not Start With Letter\"";

        public override string Description => "Model names should start with a letter so that they can be compiled to C# classes or properties.";
        
        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Warning;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            if (model.Type == TongueTwisterModel.ModelType.Localization)
            {
                ruleViolation = null;
                return false;
            }

            var formattedName = model.FormattedName ?? string.Empty;

            if (formattedName.Length == 0 || char.IsDigit(formattedName[0]))
            {
                ruleViolation = new RuleViolation()
                {
                    Code = Code,
                    Message = EditorLabels.Model.Warnings.CompilerWarningNameDoesNotStartWithLetter,
                    RuleViolationSeverity = ViolationLevel
                };
                return true;
            }
            
            ruleViolation = null;
            return false;
        }
    }
}