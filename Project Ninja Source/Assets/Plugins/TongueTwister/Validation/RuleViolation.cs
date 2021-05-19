namespace TongueTwister.Validation
{
    /// <summary>
    /// Represents the manifestation of an error that appears in validation.
    /// </summary>
    public class RuleViolation
    {
        /// <summary>
        /// The code of the rule which created this violation.
        /// </summary>
        public string Code;

        /// <summary>
        /// A message describing the error, warning, info, etc.
        /// </summary>
        public string Message;

        /// <summary>
        /// The level of violation severity.
        /// </summary>
        public RuleViolationSeverityType RuleViolationSeverity;
    }
}