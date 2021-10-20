using System;
using TongueTwister.StaticLabels;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when no <see cref="LocalizationManager"/> is present in the scene but one of its functions have
    /// been invoked.
    /// </summary>
    public class NoLocalizationManagerInstanceException : Exception
    {
        public NoLocalizationManagerInstanceException() : 
            base(RuntimeLabels.Exceptions.NoLocalizationManagerInstance) { }
    }
}