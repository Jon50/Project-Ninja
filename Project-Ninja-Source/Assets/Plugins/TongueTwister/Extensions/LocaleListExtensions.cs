using System.Collections.Generic;

namespace TongueTwister.Extensions
{
    /// <summary>
    /// Provides extension methods for lists of <see cref="Locale"/> objects.
    /// </summary>
    public static class LocaleListExtensions
    {
        /// <summary>
        /// Copies a list of locales by creating a new object for each given item and returning the result.
        /// </summary>
        /// <param name="list">The list to copy from.</param>
        /// <returns>A list of new objects which have the same values as the original list.</returns>
        public static List<Locale> Copy(this List<Locale> list)
        {
            var result = new List<Locale>();

            foreach (var item in list)
                result.Add(new Locale(item));

            return result;
        }
    }
}