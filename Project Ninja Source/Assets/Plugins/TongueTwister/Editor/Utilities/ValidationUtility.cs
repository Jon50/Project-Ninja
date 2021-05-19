using System;
using System.Collections.Generic;
using TongueTwister.Extensions;
using TongueTwister.Validation;

namespace TongueTwister.Editor.Utilities
{
    /// <summary>
    /// Simple utility class for additional functionality surrounding <see cref="ValidationRule"/> objects.
    /// </summary>
    public static class ValidationUtility
    {
        /// <summary>
        /// Instantiates a list of validation rules from derived types found in the assembly.
        /// </summary>
        /// <returns>List of all found validation rules.</returns>
        public static List<ValidationRule> GetAllValidationRules()
        {
            var derivedTypes = AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(ValidationRule));
            var modelRules = new List<ValidationRule>();

            foreach (var derivedType in derivedTypes)
            {
                modelRules.Add((ValidationRule) Activator.CreateInstance(derivedType));
            }
            
            return modelRules;
        }
    }
}