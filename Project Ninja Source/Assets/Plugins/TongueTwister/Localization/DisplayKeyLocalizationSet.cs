using System;
using System.Collections.Generic;
using System.Linq;
using TongueTwister.Exceptions;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// A set of <see cref="Localization"/> objects represented by a single display key string. These are often used
    /// in <see cref="LocalizationDictionary"/> objects.
    /// </summary>
    [Serializable]
    public class DisplayKeyLocalizationSet
    {
        #region Properties

        /// <summary>
        /// Whether or not this localization set has anything in the <c>Localization</c> dictionary.
        /// </summary>
        public bool HasLocalizations => _localizations != null && _localizations.Count != 0;

        /// <summary>
        /// The display key value tied to this localization set.
        /// </summary>
        public string DisplayKey => _displayKey;

        #endregion

        #region Private

        /// <summary>
        /// The display key used to access this set.
        /// </summary>
        [SerializeField] 
        private string _displayKey = "";

        /// <summary>
        /// List of localizations held in this set. 
        /// </summary>
        [SerializeField] 
        private List<Localization> _localizations = new List<Localization>();

        #endregion
        
        #region Constructors

        /// <summary>
        /// Creates a new <see cref="DisplayKeyLocalizationSet"/> given a display key, an array of languages, and an array of
        /// translated text which map to one another. The size of both the language and localization arrays must match.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        /// <param name="locales">An array of locales to add.</param>
        /// <param name="localizations">An array of localization texts to add, indexed by locale.</param>
        /// <exception cref="LocalizationSetInitializationException">Thrown when the given array of languages does not
        /// match the length of the given array of localizations.</exception>
        public DisplayKeyLocalizationSet(string displayKey, Locale[] locales, string[] localizations)
        {
            if (locales.Length != localizations.Length)
            {
                throw new LocalizationSetInitializationException();
            }

            _displayKey = displayKey;
            
            _localizations = new List<Localization>();
            
            for (int i = 0; i < locales.Length; i++)
            {
                _localizations.Add(new Localization()
                {
                    Locale = locales[i], 
                    Text = localizations[i],
                });
            }
        }

        /// <summary>
        /// Creates a new <see cref="DisplayKeyLocalizationSet"/> given a display key, an array of languages, an array of audio
        /// clips, and an array of translated text which map to one another. The size of all arrays must match.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        /// <param name="locales">An array of locales to add.</param>
        /// <param name="localizations">An array of localization texts to add, indexed by locale.</param>
        /// <param name="audioClips">An array of audio clips to add.</param>
        /// <exception cref="LocalizationSetInitializationException">Thrown when the given array of languages does not
        /// match the length of the given array of localizations.</exception>
        public DisplayKeyLocalizationSet(string displayKey, Locale[] locales, string[] localizations, AudioClip[] audioClips)
        {
            if (locales.Length != localizations.Length || locales.Length != audioClips.Length)
            {
                throw new LocalizationSetInitializationException();
            }

            _displayKey = displayKey;
            
            _localizations = new List<Localization>();
            
            for (int i = 0; i < locales.Length; i++)
            {
                _localizations.Add(new Localization()
                {
                    Locale = locales[i], 
                    Text = localizations[i],
                    AudioClip = audioClips[i]
                });
            }
        }

        /// <summary>
        /// Creates a new <see cref="DisplayKeyLocalizationSet"/> given a display key and list of localizations.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        /// <param name="localizations">A list of localizations to add.</param>
        public DisplayKeyLocalizationSet(string displayKey, List<Localization> localizations)
        {
            _displayKey = displayKey;
            _localizations = localizations;
        }

        /// <summary>
        /// Creates a new <see cref="DisplayKeyLocalizationSet"/> based on an existing one.
        /// </summary>
        /// <param name="copyFrom">The <see cref="DisplayKeyLocalizationSet"/> to clone.</param>
        public DisplayKeyLocalizationSet(DisplayKeyLocalizationSet copyFrom)
        {
            _localizations = copyFrom._localizations;
            _displayKey = copyFrom._displayKey;
        }

        /// <summary>
        /// Creates an empty <see cref="DisplayKeyLocalizationSet"/> with the given display key.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        public DisplayKeyLocalizationSet(string displayKey)
        {
            _displayKey = displayKey;
            _localizations = new List<Localization>();
        }

        /// <summary>
        /// Creates a new <see cref="DisplayKeyLocalizationSet"/> with one <see cref="Locale"/> and one <see cref="Localization"/>.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        /// <param name="locale">The locale to pair with the <see cref="localization"/> parameter.</param>
        /// <param name="localization">The <see cref="Localization"/> to pair with the <see cref="locale"/> parameter.</param>
        public DisplayKeyLocalizationSet(string displayKey, Locale locale, Localization localization)
        {
            _displayKey = displayKey;
            _localizations = new List<Localization>()
            {
                localization
            };
        }
        
        /// <summary>
        /// Creates a new <see cref="DisplayKeyLocalizationSet"/> with one <see cref="Locale"/> and a string text
        /// to instantiate a single <see cref="Localization"/> with.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        /// <param name="locale">The locale to pair with the <see cref="localizationText"/> parameter.</param>
        /// <param name="localizationText">The localization text to pair with the <see cref="locale"/> parameter.</param>
        public DisplayKeyLocalizationSet(string displayKey, Locale locale, string localizationText)
        {
            _displayKey = displayKey;
            _localizations = new List<Localization>()
            {
                new Localization()
                {
                    Locale = locale,
                    Text = localizationText
                }
            };
        }
        
        #endregion
        
        #region Utilities

        /// <summary>
        /// Gets the localization from the provided locale.
        /// </summary>
        /// <param name="locale">The locale to get the localization for.</param>
        /// <returns>The matching localization.</returns>
        /// <exception cref="NoLocalizationException">Thrown when this set doesn't have a localization for the given locale.</exception>
        public Localization Get(Locale locale)
        {
            if (!HasLocalization(locale))
            {
                throw new NoLocalizationException(locale);
            }

            for (int i = 0; i < _localizations.Count; i++)
            {
                if (_localizations[i].Locale == locale)
                {
                    return _localizations[i];
                }
            }

            throw new NoLocalizationException(locale);
        }
        
        /// <summary>
        /// Gets the localization for the given locale and formats the result with the given parameters.
        /// </summary>
        /// <param name="locale">The locale to get a translation for.</param>
        /// <param name="parameters">Optional string values to format the result with.</param>
        /// <returns>The localization text for the given result.</returns>
        /// <exception cref="NoLocalizationException">Thrown when the locale doesn't exist in this set.</exception>
        public string GetText(Locale locale, params object[] parameters)
        {
            if (!HasLocalization(locale))
            {
                throw new NoLocalizationException(locale);
            }

            var localization = "";
            
            for (int i = 0; i < _localizations.Count; i++)
            {
                if (_localizations[i].Locale == locale)
                {
                    localization = _localizations[i].Text;
                    break;
                }
            }

            if (parameters != null && parameters.Length > 0)
            {
                localization = string.Format(localization, parameters);
            }

            return localization;
        }

        /// <summary>
        /// Sets the localization for the given locale.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="localization"></param>
        public void Set(Locale locale, Localization localization)
        {
            if (!HasLocalization(locale))
            {
                _localizations.Add(localization);
            }
            else
            {
                for (int i = 0; i < _localizations.Count; i++)
                {
                    if (_localizations[i].Locale == locale)
                    {
                        _localizations[i] = localization;
                        return;
                    }
                }
            }
        }
        
        /// <summary>
        /// Sets the localization text for the given locale.
        /// </summary>
        /// <param name="locale">The locale to set a string value for.</param>
        /// <param name="value">The new localization value.</param>
        public void SetText(Locale locale, string value)
        {
            if (!HasLocalization(locale))
            {
                _localizations.Add(new Localization() { Locale = locale, Text = value });
            }
            else
            {
                _localizations.First(localization => localization.Locale == locale).Text = value;
            }
        }

        /// <summary>
        /// Get/Set for localizations in this collection.
        /// </summary>
        /// <param name="locale">The locale to use.</param>
        public Localization this[Locale locale]
        {
            get => Get(locale);
            set => Set(locale, value);
        }

        /// <summary>
        /// Checks to see if a locale is in this set.
        /// </summary>
        /// <param name="locale">The locale to check for.</param>
        /// <returns>Whether or not the locale is in this set.</returns>
        public bool HasLocalization(Locale locale)
            => _localizations.Any(t => t.Locale == locale);

        /// <summary>
        /// Gets all locales.
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Locale"/>.</returns>
        public IEnumerable<Locale> GetLocales() =>
            _localizations.Select(localization => localization.Locale);

        /// <summary>
        /// Returns the list of all localizations in this set.
        /// </summary>
        /// <returns>All localizations.</returns>
        public IEnumerable<Localization> GetAllLocalizations() => _localizations;

        #endregion
    }
}