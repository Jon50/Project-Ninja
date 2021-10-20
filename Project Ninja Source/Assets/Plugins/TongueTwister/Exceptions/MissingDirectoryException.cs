using System;
using TongueTwister.StaticLabels;

namespace TongueTwister.Exceptions
{
    /// <summary>
    /// Thrown when a folder has been used or referenced which doesn't exist.
    /// </summary>
    public class MissingDirectoryException : Exception
    {
        public MissingDirectoryException(string folder) : base(
            string.Format(
                RuntimeLabels.Exceptions.MissingLocalizationDictionary,
                folder))
        {
            
        }
    }
}