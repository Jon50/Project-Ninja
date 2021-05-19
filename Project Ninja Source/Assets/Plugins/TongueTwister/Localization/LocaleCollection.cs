using System;
using System.Collections.Generic;
using TongueTwister.Extensions;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// Simple container class for holding a list of locales.
    /// </summary>
    [Serializable]
    public class LocaleCollection
    {
        /// <summary>
        /// The list of locales contained within this collection.
        /// </summary>
        [SerializeField] public List<Locale> Locales = new List<Locale>();

        /// <summary>
        /// Creates a new locale collection.
        /// </summary>
        public LocaleCollection()
        {
            
        }

        /// <summary>
        /// Creates a new locale collection with the given locales by copying them - not maintaining the original
        /// object reference.
        /// </summary>
        /// <param name="copyFrom">The list of locales to copy from.</param>
        public LocaleCollection(List<Locale> copyFrom)
        {
            Locales = copyFrom.Copy();
        }
    }
}