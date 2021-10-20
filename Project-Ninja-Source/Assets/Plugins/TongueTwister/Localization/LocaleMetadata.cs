using System;
using TongueTwister.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace TongueTwister
{
    /// <summary>
    /// Properties that describe a <see cref="Locale"/>.
    /// </summary>
    [Serializable]
    public class LocaleMetadata : ISerializationCallbackReceiver
    {
        /// <summary>
        /// The display name of a locale as it may appear in the editor or game.
        /// </summary>
        [SerializeField] 
        public string DisplayName = "Locale Name";
        
        /// <summary>
        /// The name of a locale written or spelled as it would for the language/country it represents.
        /// </summary>
        [SerializeField] [FormerlySerializedAs("LocalizedName")]
        public string NativeName = "Localized Locale Name";
        
        /// <summary>
        /// A <see cref="UnitySystemLanguage"/> value.
        /// </summary>
        [NonSerialized]
        public SystemLanguage UnitySystemLanguage = 0;
        
        /// <summary>
        /// Any developer related notes or metadata.
        /// </summary>
        [SerializeField] 
        public string Notes;
        
        /// <summary>
        /// A custom code.
        /// </summary>
        [SerializeField] [FormerlySerializedAs("LocalizationCode")]
        public string CustomCode;
        
        /// <summary>
        /// The two character ISO 3166 country code used by a locale.
        /// </summary>
        [NonSerialized] 
        public ISO3166Alpha2 ISOCountryCode = ISO3166Alpha2.NONE;

        /// <summary>
        /// The two character ISO 639 language code used by a locale.
        /// </summary>
        [NonSerialized]
        public ISO639Alpha2 ISOLanguageCode = ISO639Alpha2.NONE;

        /// <summary>
        /// Creates a new serialized locale.
        /// </summary>
        public LocaleMetadata()
        {
            
        }
        
        /// <summary>
        /// Populates the fields for a new <see cref="LocaleMetadata"/> based on an existing one.
        /// </summary>
        /// <param name="copy">The <see cref="LocaleMetadata"/> object to copy values from.</param>
        public LocaleMetadata(LocaleMetadata copy)
        {
            DisplayName = copy.DisplayName;
            NativeName = copy.NativeName;
            UnitySystemLanguage = copy.UnitySystemLanguage; 
            CustomCode = copy.CustomCode; 
            ISOCountryCode = copy.ISOCountryCode; 
            ISOLanguageCode = copy.ISOLanguageCode;
            Notes = copy.Notes;
        }

        #region Serialization Support

        // naming schemes are maintained as if they're public variables, this makes it easier to read in JSON files.
        
        [SerializeField] 
        [FormerlySerializedAs("BackingLanguage")]
        [FormerlySerializedAs("_systemLanguage")]
        private string SystemLanguage;
        
        [SerializeField] 
        [FormerlySerializedAs("_countryCode")]
        private string CountryCode;
        
        [SerializeField]
        [FormerlySerializedAs("_languageCode")]
        private string LanguageCode;
        
        public void OnBeforeSerialize()
        {
            SystemLanguage = UnitySystemLanguage.ToString();
            CountryCode = ISOCountryCode.ToString();
            LanguageCode = ISOLanguageCode.ToString();
        }

        public void OnAfterDeserialize()
        {
            Enum.TryParse(SystemLanguage, true, out UnitySystemLanguage);
            Enum.TryParse(CountryCode, true, out ISOCountryCode);
            Enum.TryParse(LanguageCode, true, out ISOLanguageCode);
        }
        
        #endregion
    }
}