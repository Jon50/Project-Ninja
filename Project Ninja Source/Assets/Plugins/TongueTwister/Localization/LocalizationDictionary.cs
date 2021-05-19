using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TongueTwister.Exceptions;
using TongueTwister.StaticLabels;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// A dictionary of <see cref="DisplayKeyLocalizationSet"/> objects keyed by display keys.
    /// </summary>
    [Serializable]
    public class LocalizationDictionary
    {
        #region Private
        
        /// <summary>
        /// List of localization sets contained within this dictionary.
        /// </summary>
        [SerializeField]
        private List<DisplayKeyLocalizationSet> _localizationSets = new List<DisplayKeyLocalizationSet>();
        
        #endregion

        #region Properties
        
        /// <summary>
        /// All display keys in "full dot form", values all representing the actual keys for this dictionary.
        /// </summary>
        public List<string> Keys => _localizationSets.Select(localizationSet => localizationSet.DisplayKey).ToList();
        
        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new localization dictionary.
        /// </summary>
        public LocalizationDictionary()
        {
            
        }

        /// <summary>
        /// Creates a new localization dictionary based on a list of <see cref="DisplayKeyLocalizationSet"/>.
        /// </summary>
        /// <param name="localizationSets">The list of localization sets to start this localization dictionary with.
        /// </param>
        public LocalizationDictionary(List<DisplayKeyLocalizationSet> localizationSets)
        {
            _localizationSets = localizationSets;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Adds a new localization set by parsing and deserializing the given JSON file. Provides a locale in the "out"
        /// which matches the locale that was used for the set. This locale is mostly likely going to have no ID and
        /// needs to be normalized to a localization manager.
        /// </summary>
        /// <param name="file">The JSON file which contains a serialized localization set.</param>
        /// <param name="locale">The locale used to map the new localization set as a result of deserialization. This
        /// locale will not have an ID and must be normalized to a localization manager.</param>
        public void AddLocalizationSetFromJsonFile(
            string file,
            out Locale locale)
        {
            locale = null;
            
            if (string.IsNullOrWhiteSpace(file) || !File.Exists(file))
            {
                Debug.LogError(RuntimeLabels.Logging.Errors.InvalidTongueTwisterLocalizationFile);
                return;
            }
            
            var serializedLocalizationSet = LocaleLocalizationImport.FromFile(file);
            if (serializedLocalizationSet == null)
            {
                Debug.LogError(RuntimeLabels.Logging.Errors.FailedToDeserializeJsonFile);
                return;
            }

            locale = new Locale(serializedLocalizationSet.LocaleMetadata);
            
            foreach (var serializedLocalization in serializedLocalizationSet.Localizations)
            {
                if (ContainsKey(serializedLocalization.DisplayKey))
                {
                    // todo: https://trello.com/c/KBkrVAk5/18-audio-file-import - import audio files

                    if (this[serializedLocalization.DisplayKey].HasLocalization(locale))
                    {
                        this[serializedLocalization.DisplayKey][locale].Text = serializedLocalization.Localization;
                    }
                    else
                    {
                        this[serializedLocalization.DisplayKey].SetText(locale, serializedLocalization.Localization);
                    }
                }
                else
                {
                    this[serializedLocalization.DisplayKey] = 
                        new DisplayKeyLocalizationSet(
                            serializedLocalization.DisplayKey, 
                            new [] { locale }, 
                            new [] { serializedLocalization.Localization });
                }
            }
        }

        /// <summary>
        /// Adds an empty <see cref="DisplayKeyLocalizationSet"/> with the given display key name. If the display key already
        /// exists in the dictionary, it will be skipped.
        /// </summary>
        /// <param name="displayKey">The new display key to add.</param>
        public void AddEmpty(string displayKey)
        {
            if (ContainsKey(displayKey))
            {
                return;
            }

            this[displayKey] = new DisplayKeyLocalizationSet(displayKey);
        }
        
        /// <summary>
        /// Checks whether or not the list of Keys has the given key.
        /// </summary>
        /// <param name="key">Key to check if present.</param>
        /// <returns>Whether or not the key is contained with the list of Keys.</returns>
        public bool ContainsKey(string key) => Keys.Contains(key);
        
        /// <summary>
        /// Indexer provides access to a localization set mapped to the given display key.
        /// </summary>
        /// <param name="displayKey">Display key of the desired localization set.</param>
        /// <exception cref="NoDisplayKeyException">Thrown when no localization set has the given display key.</exception>
        public DisplayKeyLocalizationSet this[string displayKey]
        {
            get => Get(displayKey);
            set => Set(displayKey, value);
        }

        /// <summary>
        /// Gets a localization set matching the given display key.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        /// <returns>A localization set.</returns>
        public DisplayKeyLocalizationSet Get(string displayKey)
        {
            for (var i = 0; i < _localizationSets.Count; i++)
            {
                if (_localizationSets[i].DisplayKey == displayKey)
                    return _localizationSets[i];
            }

            throw new NoDisplayKeyException(displayKey);
        }

        /// <summary>
        /// Gets a <see cref="Localization"/> using the <see cref="LocalizationManager.CurrentLocale"/> given a
        /// display key value.
        /// </summary>
        /// <param name="displayKey">The display key for which to get the current locale's localization.</param>
        /// <returns>A <see cref="Localization"/> represented by the current locale and given display key.</returns>
        public Localization GetLocalization(string displayKey)
            => GetLocalization(displayKey, LocalizationManager.CurrentLocale);

        /// <summary>
        /// Gets a <see cref="Localization"/> given a display key and <see cref="Locale"/>.
        /// </summary>
        /// <param name="displayKey">The display key for which to get a localization.</param>
        /// <param name="locale">The locale for which to get a localization.</param>
        /// <returns>A <see cref="Localization"/> represented by the given locale and display key.</returns>
        public Localization GetLocalization(string displayKey, Locale locale)
            => Get(displayKey).Get(locale);

        /// <summary>
        /// Sets an existing localization set that matches ths given display key to the passed in one. Otherwise, if
        /// the localization set doesn't exist, it creates a new one.
        /// </summary>
        /// <param name="displayKey">The display key.</param>
        /// <param name="displayKeyLocalizationSet">The localization set.</param>
        public void Set(string displayKey, DisplayKeyLocalizationSet displayKeyLocalizationSet)
        {
            for (var i = 0; i < _localizationSets.Count; i++)
            {
                if (_localizationSets[i].DisplayKey == displayKey)
                {
                    _localizationSets[i] = displayKeyLocalizationSet;
                    return;
                }
            }

            _localizationSets.Add(new DisplayKeyLocalizationSet(displayKeyLocalizationSet));
        }

        /// <summary>
        /// Replaces all occurrences of an old locale with a new locale. Convenient for swapping errored localization
        /// sets. This does not invoke an editor tree update.
        /// </summary>
        /// <param name="oldLocale">The locale to replace.</param>
        /// <param name="newLocale">The locale that will replace the old locale.</param>
        public void ReplaceLocale(Locale oldLocale, Locale newLocale)
        {
            foreach (var localizationSet in _localizationSets)
            {
                foreach (var localization in localizationSet.GetAllLocalizations())
                {
                    if (localization.Locale == oldLocale)
                    {
                        localization.Locale = newLocale;
                    }
                }
            }
        }

        #endregion
    }
}