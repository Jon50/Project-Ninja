using TongueTwister.Models;

namespace TongueTwister.Validation
{
    /// <summary>
    /// The base class for implementing custom validation errors and warnings. To implement a custom validation error or
    /// warning, a class must simple  derive from this class. The system will do the rest!
    /// </summary>
    public abstract class ValidationRule
    {
        /// <summary>
        /// A code that represents this model, it should be unique.
        /// </summary>
        public abstract string Code { get; }
        
        /// <summary>
        /// The display name of this validation rule.
        /// </summary>
        public virtual string Name { get; }
        
        /// <summary>
        /// A useful description of what this rule does.
        /// </summary>
        public virtual string Description { get; }
        
        /// <summary>
        /// The level of violation severity if a model breaks this rule.
        /// </summary>
        public abstract RuleViolationSeverityType ViolationLevel { get; }

        /// <summary>
        /// Performs an evaluation of the model to determine whether or not an error should be given.
        /// </summary>
        /// <param name="model">The model to evaluate.</param>
        /// <param name="ruleViolation">The result of there being an error within the model.</param>
        /// <returns>Returns true if the model breaks this rule. Returns false if the model does not break this rule.
        /// </returns>
        public abstract bool CheckViolatesRule(TongueTwisterModel model, out RuleViolation ruleViolation);
    }

    /// <summary>
    /// Type used to specify the level of severity for a validation rule violation.
    /// </summary>
    public enum RuleViolationSeverityType
    {
        /// <summary>
        /// The violation is not serious and should be marked as warning.
        /// </summary>
        Warning,
        /// <summary>
        /// The violation is serious and should be marked as an error.
        /// </summary>
        Error,
    }
}