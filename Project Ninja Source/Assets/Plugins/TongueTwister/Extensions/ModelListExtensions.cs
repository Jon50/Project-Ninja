using System.Collections.Generic;
using TongueTwister.Models;

namespace TongueTwister.Extensions
{
    /// <summary>
    /// Extensions providing additional functionality to a list of <see cref="TongueTwisterModel"/> objects.
    /// </summary>
    public static class ModelListExtensions
    {
        /// <summary>
        /// Creates a list of new objects instantiated based on this existing list. The models will retain the same
        /// details and values for all fields, including ID.
        /// </summary>
        /// <param name="list">This list to copy.</param>
        /// <returns>A copied list of models.</returns>
        public static List<TongueTwisterModel> Copy(this List<TongueTwisterModel> list)
        {
            var result = new List<TongueTwisterModel>();

            foreach (var item in list)
                result.Add(new TongueTwisterModel(item));

            return result;
        }
    }
}