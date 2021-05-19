using System;
using TongueTwister.StaticLabels;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when a display key that doesn't exist is attempted to be used.
    /// </summary>
    public class NoDisplayKeyException : Exception
    {
        /// <summary>
        /// Creates the exception with the attempted display key name
        /// </summary>
        /// <param name="attemptedDisplayKey">The attempted display key name which doesn't exist.</param>
        public NoDisplayKeyException(string attemptedDisplayKey) : base(
            string.Format(
                RuntimeLabels.Exceptions.NoDisplayKeyWithName,
                attemptedDisplayKey))
        {
            
        }
        
        /// <summary>
        /// Creates the exception with the attempted display key name in the context of a given locale.
        /// </summary>
        /// <param name="attemptedDisplayKey">The attempted display key name which doesn't exist.</param>
        public NoDisplayKeyException(string attemptedDisplayKey, Locale forLocale) : base(
            string.Format(
                RuntimeLabels.Exceptions.NoDisplayKeyWithNameForLocale,
                attemptedDisplayKey,
                forLocale.Metadata.DisplayName))
        {
            
        }
    }
}