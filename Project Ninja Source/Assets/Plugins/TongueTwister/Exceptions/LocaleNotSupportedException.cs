using System;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when a locale that isn't supported has been used in some context where a locale is needed.
    /// </summary>
    public class LocaleNotSupportedException : Exception
    {
        public LocaleNotSupportedException(Locale locale) 
            : base($"Locale \"{locale.Metadata.DisplayName}\" is not supported.") {}
    }
}