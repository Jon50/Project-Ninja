using System.Collections.Generic;
using System.Linq;
using TongueTwister.Models;
using TongueTwister.Validation;

namespace TongueTwister.Extensions
{
    /// <summary>
    /// Extensions providing additional functionality to a <see cref="TongueTwisterModel"/> object.
    /// </summary>
    public static class ModelExtensions
    {
        /// <summary>
        /// Runs a rule check on this model using the given rules.
        /// </summary>
        /// <param name="model">This model.</param>
        /// <param name="rules">All rules to run an error check against.</param>
        public static void RunRuleCheck(this TongueTwisterModel model, List<ValidationRule> rules)
        {
            model.ModelRuleViolations.Clear();
            
            if (model.HasChildren)
            {
                foreach (var child in model.Children)
                {
                    child.RunRuleCheck(rules);
                }
            }
            
            foreach (var rule in rules)
            {
                if (rule.CheckViolatesRule(model, out RuleViolation modelErrorResult))
                {
                    model.ModelRuleViolations.Add(modelErrorResult);
                }
            }
        }

        /// <summary>
        /// Creates a new <see cref="Localization"/> object out of a model's content.
        /// </summary>
        /// <param name="model">This model.</param>
        /// <param name="configuredLocales">The currently configured locales to match a model's LocaleID off of.</param>
        public static Localization ToLocalization(this TongueTwisterModel model, Locale[] configuredLocales)
        {
            return new Localization()
            {
                Text = model.Text,
                AudioClip = model.AudioClip,
                AdditionalLocalizedContent = model.AdditionalLocalizedContent,
                UnityObject = model.UnityObject,
                Texture = model.Texture,
                Locale = configuredLocales.FirstOrDefault(locale => locale.Id == model.LocaleId)
            };
        }
    }
}