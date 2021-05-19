using System;
using System.Collections.Generic;
using System.Linq;

namespace TongueTwister.Extensions
{
    /// <summary>
    /// This class has been provided by Unity in the tree view examples and remains unedited from the source. The
    /// extensions are for <see cref="IEnumerable{T}"/>, providing additional ordering logic and functionality.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Orders this <see cref="IEnumerable{T}"/> given a <see cref="Func{T, K}"/> selector and whether or not to
        /// order by ascending.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="ascending"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static IOrderedEnumerable<T> Order<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector, bool ascending)
        {
            if (ascending)
            {
                return source.OrderBy(selector);
            }
            else
            {
                return source.OrderByDescending(selector);
            }
        }

        /// <summary>
        /// Orders this <see cref="IEnumerable{T}"/> prior to being called by <see cref="Order{T,TKey}"/>.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="ascending"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <returns></returns>
        public static IOrderedEnumerable<T> ThenBy<T, TKey>(this IOrderedEnumerable<T> source, Func<T, TKey> selector, bool ascending)
        {
            if (ascending)
            {
                return source.ThenBy(selector);
            }
            else
            {
                return source.ThenByDescending(selector);
            }
        }
    }
}