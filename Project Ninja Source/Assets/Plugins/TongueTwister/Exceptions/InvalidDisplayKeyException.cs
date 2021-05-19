using System;
using TongueTwister.StaticLabels;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when an invalid display key has been used - such as a null or empty string, not necessarily a
    /// display key which doesn't exist.
    /// </summary>
    public class InvalidDisplayKeyException : Exception
    {
        public InvalidDisplayKeyException() : base(RuntimeLabels.Exceptions.InvalidDisplayKey) { }
    }
}