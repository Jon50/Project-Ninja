using TongueTwister.Models;
using TongueTwister.StaticLabels;

namespace TongueTwister.Validation.Warnings
{
    /// <summary>
    /// Rule declaring that any model which shares the same name as its parent should have a C# compiler warning, as
    /// C# does not allow for fields, properties, functions, etc. of a class to share the name of their containing type.
    /// </summary>
    public class CompilerNameSameAsParentWarning : ValidationRule
    {
        public override string Code => "CNSP";
        
        public override string Name => "C# Compiler Name Warning - \"Name Is Same As Parent\"";

        public override string Description => "Model names should not be the same as their parents so that they can be compiled to C# classes or properties.";
        
        public override RuleViolationSeverityType ViolationLevel => RuleViolationSeverityType.Warning;
        
        public override bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation)
        {
            if (model.Type == TongueTwisterModel.ModelType.Localization || model.Parent == null)
            {
                ruleViolation = null;
                return false;
            }

            var formattedName = model.FormattedName;

            if (!string.IsNullOrWhiteSpace(formattedName) && model.Parent.FormattedName == formattedName)
            {
                ruleViolation = new RuleViolation()
                {
                    Code = Code,
                    Message = EditorLabels.Model.Warnings.CompilerWarningNameIsSameAsParent,
                    RuleViolationSeverity = ViolationLevel
                };
                return true;
            } 
            
            ruleViolation = null;
            return false;
        }
    }
}