using System;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when no localization exists for the given locale but an attempt to get one using the given display key
    /// was made.
    /// </summary>
    public class NoLocalizationException : Exception
    {
        public NoLocalizationException(Locale locale)
            : base($"No localization exists on this localization set for \"{locale.Metadata.DisplayName}") { }
    }
}