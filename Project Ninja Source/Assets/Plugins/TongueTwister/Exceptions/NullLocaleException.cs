using System;
using TongueTwister.StaticLabels;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when a null locale was attempted to be used.
    /// </summary>
    public class NullLocaleException : Exception
    {
        public NullLocaleException() : base(RuntimeLabels.Exceptions.NullLocale) { }
    }
}