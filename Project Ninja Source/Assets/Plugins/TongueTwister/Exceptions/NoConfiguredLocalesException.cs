using System;
using TongueTwister.StaticLabels;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when no locales have been configured, specifically for use within a <see cref="LocalizationManager"/>. 
    /// </summary>
    public class NoConfiguredLocalesException : Exception
    {
        public NoConfiguredLocalesException() :
            base(RuntimeLabels.Errors.LocalizationManager.NoConfiguredLocales)
        {
            
        }
    }
}