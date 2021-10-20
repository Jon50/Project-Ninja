using System;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Occurs when a localization set is attempted to be initialized with a mismatching amount of locales and
    /// localizations. This happens often when not every locale has a localization configured for the display key.
    /// </summary>
    public class LocalizationSetInitializationException : Exception
    {
        public LocalizationSetInitializationException() :
            base("Mismatched amount of languages and localizations passed into constructor for LocalizationSet! Check to make sure the given locale has a localization configured for each display key.")
        {
        }
    }
}