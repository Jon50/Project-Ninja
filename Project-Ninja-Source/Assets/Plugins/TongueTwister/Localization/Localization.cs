using System;
using TongueTwister.Models;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// Represents a localization between a language and some text.
    /// </summary>
    [Serializable]
    public class Localization
    {
        /// <summary>
        /// The locale represented by this localization.
        /// </summary>
        [SerializeField]
        public Locale Locale;
        
        /// <summary>
        /// The localized text.
        /// </summary>
        [SerializeField]
        public string Text;

        /// <summary>
        /// A localized audio clip tied to this localization.
        /// </summary>
        [SerializeField] 
        public AudioClip AudioClip;

        /// <summary>
        /// A texture object 
        /// </summary>
        [SerializeField] 
        public Texture Texture;

        /// <summary>
        /// A generic Unity Engine object to store localized content not supported in localizations by default.
        /// </summary>
        [SerializeField] 
        public UnityEngine.Object UnityObject;

        /// <summary>
        /// A collection of additional Unity Objects which may not fit into the category of Texture, AudioClip, or
        /// generic UnityEngine Object.
        /// </summary>
        [SerializeField] 
        public LocalizedContentCollection AdditionalLocalizedContent = new LocalizedContentCollection();
        
        /// <summary>
        /// Creates a new localization.
        /// </summary>
        public Localization() { }

        /// <summary>
        /// Creates a new <see cref="Localization"/> given some locale, display key, and localized text.
        /// </summary>
        /// <param name="locale">The <see cref="Locale"/> this <see cref="Localization"/> should reference.</param>
        /// <param name="localizedText">The localized/translated text to store.</param>
        public Localization(Locale locale, string localizedText)
        {
            Locale = locale;
            Text = localizedText;
        }

        /// <summary>
        /// Formats this localization text using the given parameters.
        /// </summary>
        /// <param name="parameters">The parameters used for formatting the string.</param>
        /// <returns>Formatted localization text. text.</returns>
        public string Format(params object[] parameters)
        {
            return string.Format(Text, parameters);
        }

        /// <summary>
        /// Formats this localization text using the given parameters.
        /// </summary>
        /// <param name="parameters">The parameters used for formatting the string.</param>
        /// <returns>Formatted localization text. text.</returns>
        public string Format(params string[] parameters)
        {
            return Format(parameters as object[]);
        }
    }
}