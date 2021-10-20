using System;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// A single display key and its translated localization text.
    /// </summary>
    [Serializable]
    public class DisplayKeyLocalizationImport
    {
        /// <summary>
        /// The full display key path.
        /// </summary>
        [SerializeField] public string DisplayKey = "";

        /// <summary>
        /// The translated localization text.
        /// </summary>
        [SerializeField] public string Localization = "";
        
        public DisplayKeyLocalizationImport() { }

        public DisplayKeyLocalizationImport(string displayKey, string localization)
        {
            DisplayKey = displayKey;
            Localization = localization;
        }
    }
}