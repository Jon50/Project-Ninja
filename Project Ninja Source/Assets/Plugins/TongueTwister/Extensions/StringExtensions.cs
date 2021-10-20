using TongueTwister.Models;
using UnityEngine;

namespace TongueTwister.Extensions
{
    /// <summary>
    /// Extensions for strings in relation to the localization system.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Gets a <see cref="Localization"/> from the current <see cref="LocalizationManager"/> and current
        /// <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <returns>A <see cref="Localization"/>.</returns>
        public static Localization GetLocalization(this string displayKey)
            => LocalizationManager.GetLocalization(displayKey);
        
        /// <summary>
        /// Gets a <see cref="Localization"/> from the current <see cref="LocalizationManager"/> using the given a
        /// <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <returns>A <see cref="Localization"/>.</returns>
        public static Localization GetLocalization(this string displayKey, Locale locale)
            => LocalizationManager.GetLocalization(displayKey, locale);

        /// <summary>
        /// Gets a <see cref="Localization"/>'s text value from the current <see cref="LocalizationManager"/> and
        /// current <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <returns>A <see cref="Localization"/>'s text value.</returns>
        public static string GetLocalizationText(this string displayKey)
            => displayKey?.GetLocalization()?.Text;

        /// <summary>
        /// Gets a <see cref="Localization"/>'s text value from the current <see cref="LocalizationManager"/> and
        /// current <see cref="Locale"/>, then formats using the provided <see cref="parameters"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="parameters">The <see cref="object"/> parameters array to format the text result with.</param>
        /// <returns>A formatted <see cref="Localization"/>'s text value.</returns>
        public static string GetLocalizationText(this string displayKey, params object[] parameters)
            => displayKey?.GetLocalization()?.Format(parameters);

        /// <summary>
        /// Gets a <see cref="Localization"/>'s text value from the current <see cref="LocalizationManager"/> using the
        /// given a <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <returns>A <see cref="Localization"/>'s text value.</returns>
        public static string GetLocalizationText(this string displayKey, Locale locale)
            => displayKey?.GetLocalization(locale)?.Text;

        /// <summary>
        /// Gets a <see cref="Localization"/>'s text value from the current <see cref="LocalizationManager"/> using the
        /// given <see cref="Locale"/> then formats it using the provided <see cref="object"/> <see cref="parameters"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <param name="parameters">The <see cref="object"/> parameters array to format the text result with.</param>
        /// <returns>A formatted <see cref="Localization"/>'s text value.</returns>
        public static string GetLocalizationText(this string displayKey, Locale locale, params object[] parameters)
            => displayKey?.GetLocalization(locale)?.Format(parameters);

        /// <summary>
        /// Gets a <see cref="Localization"/>'s <see cref="AudioClip"/> value from the current
        /// <see cref="LocalizationManager"/> and current <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <returns>A <see cref="Localization"/>'s <see cref="AudioClip"/>.</returns>
        public static AudioClip GetLocalizationAudioClip(this string displayKey)
            => displayKey?.GetLocalization()?.AudioClip;

        /// <summary>
        /// Gets a <see cref="Localization"/>'s <see cref="AudioClip"/> value from the current
        /// <see cref="LocalizationManager"/> and given a <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <returns>A <see cref="Localization"/>'s <see cref="AudioClip"/>.</returns>
        public static AudioClip GetLocalizationAudioClip(this string displayKey, Locale locale)
            => displayKey?.GetLocalization(locale)?.AudioClip;

        /// <summary>
        /// Gets a <see cref="Localization"/>'s <see cref="Texture"/> value from the current
        /// <see cref="LocalizationManager"/> and current <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <returns>A <see cref="Localization"/>'s <see cref="Texture"/>.</returns>
        public static Texture GetLocalizationTexture(this string displayKey)
            => displayKey?.GetLocalization()?.Texture;

        /// <summary>
        /// Gets a <see cref="Localization"/>'s <see cref="Texture"/> value from the current
        /// <see cref="LocalizationManager"/> and given <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <returns>A <see cref="Localization"/>'s <see cref="Texture"/>.</returns>
        public static Texture GetLocalizationTexture(this string displayKey, Locale locale)
            => displayKey?.GetLocalization(locale)?.Texture;
        
        /// <summary>
        /// Gets a <see cref="Localization"/>'s <see cref="UnityEngine.Object"/> value from the current
        /// <see cref="LocalizationManager"/> and current <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <returns>A <see cref="Localization"/>'s <see cref="UnityEngine.Object"/>.</returns>returns>
        public static UnityEngine.Object GetLocalizationUnityObject(this string displayKey)
            => displayKey?.GetLocalization()?.UnityObject;

        /// <summary>
        /// Gets a <see cref="Localization"/>'s <see cref="UnityEngine.Object"/> value from the current
        /// <see cref="LocalizationManager"/> and given a <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <returns>A <see cref="Localization"/>'s <see cref="UnityEngine.Object"/>.</returns>returns>
        public static UnityEngine.Object GetLocalizationUnityObject(this string displayKey, Locale locale)
            => displayKey?.GetLocalization(locale)?.UnityObject;

        /// <summary>
        /// Gets a <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> value from the current
        /// <see cref="LocalizationManager"/> and current <see cref="Locale"/>, using an identifier string to identify
        /// the item by a name defined in the editor.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="identifier">The string identifier of the additional localized content to get. These values are
        /// set inside of the TongueTwister Window's localization editor.</param>
        /// <returns>A <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> item.</returns>
        public static LocalizedContent GetLocalizationAdditionalContent(this string displayKey, string identifier)
            => displayKey?.GetLocalization()?.AdditionalLocalizedContent?[identifier];

        /// <summary>
        /// Gets a <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> value from the current
        /// <see cref="LocalizationManager"/>, given a <see cref="Locale"/> and identifier string to identify
        /// the item by a name defined in the editor.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="identifier">The string identifier of the additional localized content to get. These values are
        /// set inside of the TongueTwister Window's localization editor.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <returns>A <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> item.</returns>
        public static LocalizedContent GetLocalizationAdditionalContent(this string displayKey, string identifier, Locale locale)
            => displayKey?.GetLocalization(locale)?.AdditionalLocalizedContent?[identifier];
        
        /// <summary>
        /// Gets a <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> value from the current
        /// <see cref="LocalizationManager"/> and current <see cref="Locale"/>, using an index to identify the item by
        /// its position in the parent collection.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="index">The index of the additional localized content item in its parent container. This value
        /// is used like an array accessor.</param>
        /// <returns>A <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> item.</returns>
        public static LocalizedContent GetLocalizationAdditionalContent(this string displayKey, int index)
            => displayKey?.GetLocalization()?.AdditionalLocalizedContent?[index];

        /// <summary>
        /// Gets a <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> value from the current
        /// <see cref="LocalizationManager"/>, given a <see cref="Locale"/> and index to identify the item by its
        /// position in the parent collection.
        /// </summary>
        /// <param name="displayKey">This string which represents a display key value.</param>
        /// <param name="index">The index of the additional localized content item in its parent container. This value
        /// is used like an array accessor.</param>
        /// <param name="locale">The <see cref="Locale"/> to get a <see cref="Localization"/> for.</param>
        /// <returns>A <see cref="Localization"/>'s additional <see cref="LocalizedContent"/> item.</returns>
        public static LocalizedContent GetLocalizationAdditionalContent(this string displayKey, int index, Locale locale)
            => displayKey?.GetLocalization(locale)?.AdditionalLocalizedContent?[index];
    }
}