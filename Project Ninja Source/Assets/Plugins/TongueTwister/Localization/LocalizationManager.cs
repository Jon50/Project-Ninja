using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TongueTwister.Exceptions;
using TongueTwister.Models;
using TongueTwister.Pathbuilders;
using TongueTwister.StaticLabels;
using TongueTwister.Utilities;
using UnityEngine;

namespace TongueTwister
{
    /// <summary>
    /// Manages all loaded localizations (both configured and external ones). Used to retrieve localized string data.
    /// </summary>
    public class LocalizationManager : MonoBehaviour
    {
        #region Static References
        
        /// <summary>
        /// Public static reference of the <see cref="LocalizationManager"/>.
        /// </summary>
        public static LocalizationManager instance { get; set; }

        /// <summary>
        /// The current locale of the system. Assigning a value to the CurrentLocale calls
        /// <see cref="SetCurrentLocale"/> and in turn invokes the <see cref="OnCurrentLocaleChanged"/> event. If the
        /// new locale value is valid, it will be updated.
        /// </summary>
        public static Locale CurrentLocale
        {
            get => instance._currentLocale;
            set => instance.SetCurrentLocale(value);
        }
            
        /// <summary>
        /// Denotes when the localization manager is ready to provide localizations.
        /// </summary>
        public static bool Ready { get; private set; }

        /// <summary>
        /// Invoked after <see cref="CurrentLocale"/> is changed.
        /// </summary>
        public static event Action OnCurrentLocaleChanged;

        /// <summary>
        /// Invoked after the <see cref="LocalizationManager"/> has initialized.
        /// </summary>
        public static event Action OnInitialized;

        /// <summary>
        /// Invoked after external locales are dumped and reloaded. Particularly when calls are made to
        /// <see cref="ReloadExternalLocalizations"/> on the current <see cref="LocalizationManager"/>.
        /// </summary>
        public static event Action OnExternalLocalesReloaded;

        #endregion

        #region Editor Debug Settings

        /// <summary>
        /// Expands the debug settings in the editor. This isn't used anywhere else.
        /// </summary>
        [HideInInspector]
        [SerializeField] private bool _expandDebugSettings;
        
        /// <summary>
        /// Enables the in-game debug window. 
        /// </summary>
        [HideInInspector]
        [SerializeField] private bool _enableInGameDebugWindow = false;

        /// <summary>
        /// The debug window's ID.
        /// </summary>
        [HideInInspector]
        [SerializeField] private int _inGameDebugWindowId = 7731512;

        #endregion

        #region Configuration Settings

        /// <summary>
        /// When enabled, saves the "last used locale" whenever the value of <see cref="CurrentLocale"/> has been
        /// changed or updated.
        /// </summary>
        [SerializeField] [HideInInspector]
        private bool _saveLastUsedLocaleToPlayerPrefs = true;

        /// <summary>
        /// The player pref key which saves the value of the last used locale.
        /// </summary>
        [SerializeField] [HideInInspector] 
        private string _lastUsedLocalePlayerPrefKey = "TT:LastUsedPlayerLocale";

        /// <summary>
        /// When enabled, will look in the configured filepath relative to the application path for files that may
        /// contain additional localizations for unsupported languages.
        /// </summary>
        [SerializeField] [HideInInspector]
        private bool _loadAdditionalLocalizationFiles = false;

        /// <summary>
        /// Used to get the path where additional localizations are meant to be stored.
        /// </summary>
        [SerializeField] [HideInInspector] 
        private FolderPathBuilder _additionalLocalizationsFolderPathBuilder;

        /// <summary>
        /// The default locale which the system will start on.
        /// </summary>
        [SerializeField] [HideInInspector] 
        private string _defaultLocaleId;

        /// <summary>
        /// The collection of models generated from the tree view.
        /// </summary>
        [SerializeField] [HideInInspector]
        private TongueTwisterModelCollection _modelCollection;

        /// <summary>
        /// Locales configured for use within the LocalizationManager.
        /// </summary>
        [SerializeField] [HideInInspector]
        private LocaleCollection _configuredLocalesCollection = new LocaleCollection();
        
        #endregion

        #region Instance Properties

        /// <summary>
        /// The folder path where additional TongueTwister localization files may exist.
        /// </summary>
        public string AdditionalLocalizationsFolderPath => _additionalLocalizationsFolderPathBuilder?.BuildPath() ?? "";

        /// <summary>
        /// All locales including both the ones configured by developers and the ones loaded from external files.
        /// </summary>
        public List<Locale> AllLocales
        {
            get
            {
                var results = new List<Locale>();
                results.AddRange(ConfiguredLocales);
                results.AddRange(ExternalLoadedLocales);
                return results;
            }
        }

        /// <summary>
        /// True when the current locale has been loaded from an external source.
        /// </summary>
        public bool CurrentLocaleIsExternal => ExternalLoadedLocales.Contains(CurrentLocale);

        /// <summary>
        /// Locales configured for use within the LocalizationManager.
        /// </summary>
        public List<Locale> ConfiguredLocales => _configuredLocalesCollection?.Locales;

        /// <summary>
        /// Locales that were externally loaded into the LocalizationManager through the file system or other means.
        /// </summary>
        public List<Locale> ExternalLoadedLocales => _externalLoadedLocalesCollection?.Locales;

        /// <summary>
        /// The locale that the system defaults to.
        /// </summary>
        public Locale DefaultLocale => ConfiguredLocales.FirstOrDefault(locale => locale.Id == _defaultLocaleId);

        #endregion

        #region Instance Private Data
        
        /// <summary>
        /// The currently used locale.
        /// </summary>
        private Locale _currentLocale = null;

        /// <summary>
        /// List of locales loaded from external files or methods.
        /// </summary>
        private LocaleCollection _externalLoadedLocalesCollection = new LocaleCollection(); 
            
        /// <summary>
        /// The collection of localizations configured in the Editor. This dictionary contains no localizations which
        /// may have been imported from external files.
        /// </summary>
        private LocalizationDictionary _configuredLocalizationDictionary;
        
        /// <summary>
        /// The collection of localizations imported from external files.
        /// </summary>
        private LocalizationDictionary _externalLocalizationDictionary;

        #endregion

        #region Unity Events

        /// <summary>
        /// Unity event called whenever this class is enabled. It will assign the <see cref="instance"/> to itself,
        /// establishing a singleton. Otherwise, if the instance is not null and the instance is not this object
        /// reference, the game object will destroy itself. Invokes <see cref="Initialize"/>.
        /// </summary>
        private void OnEnable()
        {
            if (instance && instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            DontDestroyOnLoad(instance = this);
            Initialize();
        }

        /// <summary>
        /// Unity event called whenever this game object is destroyed.
        /// </summary>
        private void OnDestroy()
        {
            if (instance && instance != this)
            {
                return; 
            }

            Ready = false;
        }

        /// <summary>
        /// Unity event called to draw GUI. This function will draw the GUI window if
        /// <see cref="_enableInGameDebugWindow"/> is enabled. Invokes <see cref="DebugWindow"/>. This functions only
        /// purpose is to show the in-game debug window UI if enabled.
        /// </summary>
        private void OnGUI()
        {
            if (!_enableInGameDebugWindow) return;

            var rect = new Rect(0, 0, Screen.width, 150);

            GUI.Window(_inGameDebugWindowId, rect, DebugWindow, "Localization Manager Debugger");
        }

        #endregion

        #region Initialization

        /// <summary>
        /// Initializes the localization manager by loading player preferences and creating the localization dictionary.
        /// </summary>
        private void Initialize()
        {
            Ready = false;

            InitializeConfiguredLocalesCollection();
            InitializeExternalLocalesCollection();
            InitializeConfiguredLocalizationDictionary();
            InitializeCurrentLocale();
            
            Ready = true;
            OnInitialized?.Invoke();
        }

        /// <summary>
        /// Sets <see cref="CurrentLocale"/> based on configuration settings and what is available.
        /// </summary>
        /// <exception cref="Exception">Throw when no locales are set.</exception>
        private void InitializeCurrentLocale()
        {
            if (_saveLastUsedLocaleToPlayerPrefs && PlayerPrefs.HasKey(_lastUsedLocalePlayerPrefKey))
            {
                var lastUsedLocale = GetLastUsedLocaleFromPlayerPrefs();
                
                if (lastUsedLocale != null)
                {
                    CurrentLocale = lastUsedLocale;
                    return;
                }
            }

            try
            {
                CurrentLocale = DefaultLocale;
                return;
            }
            catch
            {
                Debug.LogWarning(RuntimeLabels.Logging.Warnings.LocalizationManager.NoDefaultLocale);
            }
            
            var firstLocale = AllLocales.FirstOrDefault();

            if (firstLocale != null)
            {
                CurrentLocale = firstLocale;
                return;
            }
            
            throw new NoConfiguredLocalesException();
        }

        /// <summary>
        /// Creates a localization dictionary out of the serialized model collection stored in this asset. This
        /// localization dictionary contains all of the configured display keys made within the editor.
        /// </summary>
        private void InitializeConfiguredLocalizationDictionary()
        {
            _modelCollection = _modelCollection ?? new TongueTwisterModelCollection();
            var builtModelTree = ModelUtility.ListToTree(_modelCollection.Models);
            _configuredLocalizationDictionary = ModelUtility.TreeToLocalizationDictionary(
                builtModelTree, 
                _configuredLocalesCollection.Locales.ToArray());
        }

        /// <summary>
        /// Ensures the configured locales list is not null.
        /// </summary>
        private void InitializeConfiguredLocalesCollection()
        {
            _configuredLocalesCollection = _configuredLocalesCollection ?? new LocaleCollection();
        }

        /// <summary>
        /// Loads localizations from external files provided <see cref="_loadAdditionalLocalizationFiles"/> is enabled.
        /// </summary>
        private void InitializeExternalLocalesCollection()
        {
            if (!_loadAdditionalLocalizationFiles)
            {
                return;
            }
            
            try
            {
                LoadLocalizationsFromExternalFiles();
            }
            catch (Exception exception)
            {
                Debug.LogError(exception.Message);
            }
        }

        #endregion

        #region Private Utilities

        /// <summary>
        /// Gets the last used locale by the name stored in <c>PlayerPrefs</c>. If a locale does not exist on this
        /// localization manager with the found name, null is returned.
        /// </summary>
        /// <returns>The last used locale or null.</returns>
        private Locale GetLastUsedLocaleFromPlayerPrefs()
        {
            var lastLocaleId = PlayerPrefs.GetString(_lastUsedLocalePlayerPrefKey);

            return string.IsNullOrWhiteSpace(lastLocaleId) 
                ? null 
                : AllLocales.FirstOrDefault(locale => locale.Id == lastLocaleId);
        }

        /// <summary>
        /// Loads localizations from external files using the <c>Application.dataPath</c> combined with the
        /// <c>_relativeExternalLocalizationFilePath</c>. This will override existing localizations if a supported
        /// language is already loaded (including default languages).
        /// </summary>
        private void LoadLocalizationsFromExternalFiles()
        {
            if (_additionalLocalizationsFolderPathBuilder == null)
            {
                _additionalLocalizationsFolderPathBuilder = ScriptableObject.CreateInstance<ApplicationDataFolderPathBuilder>();
            }

            _externalLoadedLocalesCollection = new LocaleCollection();
            _externalLocalizationDictionary = new LocalizationDictionary();

            var additionalLocalizationsFolderPath = AdditionalLocalizationsFolderPath;
            
            if (!Directory.Exists(additionalLocalizationsFolderPath))
            {
                throw new MissingDirectoryException(additionalLocalizationsFolderPath);
            }

            var files = TongueTwisterFileUtil.GetFiles(additionalLocalizationsFolderPath, "*.json");

            foreach (var file in files)
            {
                Import(LocaleLocalizationImport.FromFile(file));
                
                Debug.Log(
                    string.Format(
                        RuntimeLabels.Logging.LoadedTongueTwisterLocalizationFile,
                        file));
            }
        }

        /// <summary>
        /// Gets a list of locales based on the given <see cref="LocaleSearchType"/>. 
        /// </summary>
        /// <param name="localeSearchType"></param>
        /// <returns></returns>
        private List<Locale> GetLocalesBySearchType(LocaleSearchType localeSearchType)
        {
            switch (localeSearchType)
            {
                default:
                    Debug.LogError($"Locale search type not yet supported: {localeSearchType}");
                    return null;
                case LocaleSearchType.ExternalLoadedLocales: return ExternalLoadedLocales;
                case LocaleSearchType.ConfiguredLocales: return ConfiguredLocales;
                case LocaleSearchType.AllLocales: return AllLocales;
            }
        }

        #endregion
        
        #region Public Utilities

        /// <summary>
        /// Gets a localization given a display key and the <see cref="CurrentLocale"/>.
        /// </summary>
        /// <param name="displayKey">The display key to get a localization for.</param>
        /// <returns>A localization for the display key and current locale.</returns>
        public static Localization GetLocalization(string displayKey)
            => GetLocalization(displayKey, CurrentLocale);
        
        /// <summary>
        /// Gets a localization given a display key and locale.
        /// </summary>
        /// <param name="displayKey">The display key to get a localization for.</param>
        /// <param name="locale">The locale to get a localization for.</param>
        /// <returns>A localization for the display key and given locale.</returns>
        /// <exception cref="NoLocalizationManagerInstanceException">Thrown when no LocalizationManager instance is instantiated.
        /// </exception>
        /// <exception cref="NoDisplayKeyException">Thrown when the given display key does not exist in the external
        /// localizations or configured localizations.</exception>
        /// <exception cref="LocaleNotSupportedException">Thrown when the given locale does not exist in the
        /// external locales list or configured locale list.</exception>
        public static Localization GetLocalization(string displayKey, Locale locale)
        {
            if (!instance)
            {
                throw new NoLocalizationManagerInstanceException();
            }

            if (locale == null)
            {
                throw new NullLocaleException();
            }

            if (string.IsNullOrWhiteSpace(displayKey))
            {
                throw new InvalidDisplayKeyException();
            }
            
            if (instance._externalLoadedLocalesCollection.Locales.Contains(locale))
            {
                if (instance._externalLocalizationDictionary[displayKey].HasLocalization(locale)) 
                    return instance._externalLocalizationDictionary[displayKey][locale];
            }
            
            if (instance._configuredLocalesCollection.Locales.Contains(locale))
            {
                if (instance._configuredLocalizationDictionary[displayKey].HasLocalization(locale))
                    return instance._configuredLocalizationDictionary[displayKey][locale];
                
                Debug.LogException(new NoDisplayKeyException(displayKey, locale));
                return null;
            }
            
            Debug.LogException(new LocaleNotSupportedException(locale));
            return null;
        }

        /// <summary>
        /// Safely removes all external localizations and sets the current locale to the default locale if it's
        /// currently set to one of the externally loaded locales.
        /// </summary>
        public void DumpExternalLocalizations()
        {
            if (_externalLoadedLocalesCollection.Locales.Count != 0)
            {
                if (CurrentLocaleIsExternal)
                {
                    try
                    {
                        SetCurrentLocale(DefaultLocale);
                    }
                    catch
                    {
                        SetCurrentLocale(ConfiguredLocales.FirstOrDefault());
                    }
                }
            }

            if (_externalLocalizationDictionary != null && _externalLocalizationDictionary.Keys.Count > 0)
            {
                _externalLocalizationDictionary = null;
            }
        }

        /// <summary>
        /// Calls <see cref="DumpExternalLocalizations"/> and then the private function
        /// <see cref="LoadLocalizationsFromExternalFiles"/>.
        /// </summary>
        public void ReloadExternalLocalizations()
        {
            DumpExternalLocalizations();
            LoadLocalizationsFromExternalFiles();
            OnExternalLocalesReloaded?.Invoke();
        }

        /// <summary>
        /// Gets a <see cref="TongueTwisterModel"/> from the private <see cref="TongueTwisterModelCollection"/> deserialized on this Localization
        /// Manager.
        /// </summary>
        /// <param name="id">The ID of the model to get.</param>
        /// <returns>A model with the given ID, otherwise null if one isn't found.</returns>
        public TongueTwisterModel GetModelById(int id)
        {
            return _modelCollection.Models.FirstOrDefault(model => model.Id == id);
        }

        /// <summary>
        /// Gets a locale matching the given ID.
        /// </summary>
        /// <param name="id">ID of the locale to get.</param>
        /// <param name="includeExternalLocales">Whether or not to include looking at external locales.</param>
        /// <returns>The locale matching the given ID or null if one doesn't exist.</returns>
        public Locale GetLocaleById(string id, bool includeExternalLocales = true)
        {
            foreach (var locale in _configuredLocalesCollection.Locales)
            {
                if (locale.Id == id) return locale;
            }

            if (includeExternalLocales)
            {
                foreach (var locale in _externalLoadedLocalesCollection.Locales)
                {
                    if (locale.Id == id) return locale;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a list of all display keys which have been configured within the editor.
        /// </summary>
        /// <returns>List of display keys.</returns>
        public List<string> GetConfiguredDisplayKeys() => _configuredLocalizationDictionary.Keys;
        
        /// <summary>
        /// Sets the current locale, updates the <c>PlayerPrefs</c>, and invokes <see cref="OnCurrentLocaleChanged"/>
        /// </summary>
        /// <param name="locale">The locale that the system should change to.</param>
        public void SetCurrentLocale(Locale locale)
        {
            if (locale == null)
            {
                throw new Exception(RuntimeLabels.Errors.LocalizationManager.NullSetLocale);
            }
            
            if (_saveLastUsedLocaleToPlayerPrefs)
            {
                PlayerPrefs.SetString(_lastUsedLocalePlayerPrefKey, locale?.Id ?? string.Empty);
                PlayerPrefs.Save();
            }

            _currentLocale = locale;
            OnCurrentLocaleChanged?.Invoke();
        }

        /// <summary>
        /// Gets the first or default locale that matches the given <see cref="ISO3166Alpha2"/> and
        /// <see cref="ISO639Alpha2"/>. If there are several locales that match the given criteria, they will be
        /// ignored. To see all results, it's recommended to use <see cref="GetLocalesByIsoCode"/>.
        /// </summary>
        /// <param name="countryCode">The <see cref="ISO3166Alpha2"/> country code to get.</param>
        /// <param name="languageCode">The <see cref="ISO639Alpha2"/> language code to get.</param>
        /// <param name="localeSearchType">The <see cref="LocaleSearchType"/> to constrain results to.</param>
        /// <returns>A locale matching the given country and language code. If one isn't found, null is returned.</returns>
        public Locale GetLocaleByIsoCode(
            ISO3166Alpha2 countryCode, ISO639Alpha2 languageCode,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            // note: this may be due for a refactor at some point.
            return 
                GetLocalesBySearchType(localeSearchType)
                    .FirstOrDefault(locale => 
                        CompareIsoCodes(
                            locale.Metadata, 
                            new LocaleMetadata() { ISOCountryCode = countryCode, ISOLanguageCode = languageCode}));
        }

        /// <summary>
        /// Gets the first locale that matches the given <see cref="SystemLanguage"/>. If there are several locales
        /// that match the given <see cref="SystemLanguage"/>, they will be ignored. To see all results, it's
        /// recommended to use <see cref="GetLocalesBySystemLanguage"/>.
        /// </summary>
        /// <param name="systemLanguage">The <see cref="SystemLanguage"/> to get.</param>
        /// <param name="localeSearchType">The <see cref="LocaleSearchType"/> to constrain results to.</param>
        /// <returns>A locale matching the given system language. If one isn't found, null is returned.</returns>
        public Locale GetLocaleBySystemLanguage(SystemLanguage systemLanguage,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return 
                GetLocalesBySearchType(localeSearchType)
                    .FirstOrDefault(locale =>
                        locale.Metadata.UnitySystemLanguage == systemLanguage);
        }

        /// <summary>
        /// Gets the first locale that matches the given display name. If there are several locales that match the
        /// given display name, they will be ignored. To see all results, it's recommended to use
        /// <see cref="SearchLocalesByName"/>. However this may include locales matching over native name as well.
        /// </summary>
        /// <param name="displayName">The display name to get a locale with.</param>
        /// <param name="localeSearchType">The <see cref="LocaleSearchType"/> to constrain results to.</param>
        /// <returns>The first locale matching the given display name.</returns>
        public Locale GetLocaleByDisplayName(string displayName, 
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return GetLocalesBySearchType(localeSearchType)
                .FirstOrDefault(locale => locale.Metadata.DisplayName == displayName);
        }

        /// <summary>
        /// Gets the first locale that matches the given native name. If there are several locales that match the
        /// given native name, they will be ignored. To see all results, it's recommended to use
        /// <see cref="SearchLocalesByName"/>. However this may include locales matching over display name as well.
        /// </summary>
        /// <param name="nativeName">The native name to get a locale with.</param>
        /// <param name="localeSearchType">The <see cref="LocaleSearchType"/> to constrain results to.</param>
        /// <returns>The first locale matching the given native name.</returns>
        public Locale GetLocaleByNativeName(string nativeName,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return GetLocalesBySearchType(localeSearchType)
                .FirstOrDefault(locale => locale.Metadata.NativeName == nativeName);
        }

        /// <summary>
        /// Gets all locales that match the given country code and language code.
        /// </summary>
        /// <param name="countryCode">The <see cref="ISO3166Alpha2"/> country code to get.</param>
        /// <param name="languageCode">The <see cref="ISO639Alpha2"/> language code to get.</param>
        /// <param name="localeSearchType">The <see cref="LocaleSearchType"/> to constrain results to.</param>
        /// <returns>All locales matching the given country and language code.</returns>
        public IEnumerable<Locale> GetLocalesByIsoCode(ISO3166Alpha2 countryCode, ISO639Alpha2 languageCode,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            // note: this may be due for a refactor at some point.
            return 
                GetLocalesBySearchType(localeSearchType)
                    .Where(locale =>
                        CompareIsoCodes(
                            locale.Metadata, 
                            new LocaleMetadata() { ISOCountryCode = countryCode, ISOLanguageCode = languageCode }));
        }

        /// <summary>
        /// Gets all locales that match the given <see cref="SystemLanguage"/>.
        /// </summary>
        /// <param name="systemLanguage">The <see cref="SystemLanguage"/> to get.</param>
        /// <param name="localeSearchType">The <see cref="LocaleSearchType"/> to constrain results to.</param>
        /// <returns>All locales matching the given system language.</returns>
        public IEnumerable<Locale> GetLocalesBySystemLanguage(SystemLanguage systemLanguage,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            // note: this may be due for a refactor at some point.
            return 
                GetLocalesBySearchType(localeSearchType)
                    .Where(locale =>
                        locale.Metadata.UnitySystemLanguage == systemLanguage);
        }

        /// <summary>
        /// Searches for all <see cref="Locale"/> objects which match over
        /// display name <see cref="StringComparison.OrdinalIgnoreCase"/> and
        /// native name <see cref="StringComparison.InvariantCultureIgnoreCase"/>.
        /// </summary>
        /// <param name="searchByName">The input name/text to search with.</param>
        /// <param name="localeSearchType">The <see cref="LocaleSearchType"/> to constrain results to.</param>
        /// <returns>All matching locales.</returns>
        public IEnumerable<Locale> SearchLocalesByName(string searchByName,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return
                GetLocalesBySearchType(localeSearchType)
                    .Where(locale => 
                        CompareDisplayNames(searchByName, locale.Metadata.DisplayName) ||
                        CompareNativeNames(searchByName, locale.Metadata.NativeName));
        }

        /// <summary>
        /// Imports all the content from a <see cref="LocaleLocalizationImport"/> and adds it to the externally loaded
        /// localization dictionary.
        /// </summary>
        /// <param name="localeLocalizationImport">The import content.</param>
        public void Import(LocaleLocalizationImport localeLocalizationImport)
        {
            if (localeLocalizationImport.Localizations == null ||
                localeLocalizationImport.Localizations.Length == 0)
            {
                return;
            }
            
            var mostSimilarLocale = GetSimilarLocale(localeLocalizationImport.LocaleMetadata);

            if (mostSimilarLocale == null)
            {
                // no similar locale was found so a new one can be added to the list of externally loaded locales.
                mostSimilarLocale = new Locale(localeLocalizationImport.LocaleMetadata);
                _externalLoadedLocalesCollection.Locales.Add(mostSimilarLocale);
            }
                
            for (int i = 0; i < localeLocalizationImport.Localizations.Length; i++)
            {
                var displayKey = localeLocalizationImport.Localizations[i].DisplayKey;
                var localizationText = localeLocalizationImport.Localizations[i].Localization;

                if (!_externalLocalizationDictionary.ContainsKey(displayKey))
                {
                    _externalLocalizationDictionary[displayKey] = 
                        new DisplayKeyLocalizationSet(displayKey, mostSimilarLocale, localizationText);
                }
                else
                {
                    _externalLocalizationDictionary[displayKey][mostSimilarLocale] =
                        new Localization(mostSimilarLocale, localizationText);
                }
            }

            OnImportedExternalLocalizations(localeLocalizationImport);
        }

        #endregion

        #region Obsolete Public Utilities
        
        /// <summary>
        /// Please use <see cref="GetLocalization(string)"/>.
        /// </summary>
        [Obsolete("Use GetLocalization(). All specific accessors for localization fields are moving to string extensions.")]
        public static string GetLocalizationText(string displayKey)
            => GetLocalizationText(displayKey, CurrentLocale);

        /// <summary>
        /// Please use <see cref="GetLocalization(string)"/>.
        /// </summary>
        [Obsolete("Use GetLocalization(). All specific accessors for localization fields are moving to string extensions.")]
        public static string GetLocalizationText(string displayKey, params object[] parameters)
            => GetLocalizationText(displayKey, CurrentLocale, parameters);

        /// <summary>
        /// Please use <see cref="GetLocalization(string)"/>.
        /// </summary>
        [Obsolete("Use GetLocalization(). All specific accessors for localization fields are moving to string extensions.")]
        public static string GetLocalizationText(string displayKey, Locale locale)
            => GetLocalizationText(displayKey, locale, new object[0]);

        /// <summary>
        /// Please use <see cref="GetLocalization(string)"/>.
        /// </summary>
        [Obsolete("Use GetLocalization(). All specific accessors for localization fields are moving to string extensions.")]
        public static string GetLocalizationText(string displayKey, Locale locale, params object[] parameters)
            => GetLocalization(displayKey, locale)?.Format(parameters);

        /// <summary>
        /// Please use <see cref="GetLocalization(string)"/>.
        /// </summary>
        [Obsolete("Use GetLocalization(). All specific accessors for localization fields are moving to string extensions.")]
        public static AudioClip GetLocalizationAudioClip(string displayKey)
            => GetLocalizationAudioClip(displayKey, CurrentLocale);

        /// <summary>
        /// Please use <see cref="GetLocalization(string)"/>.
        /// </summary>
        [Obsolete("Use GetLocalization(). All specific accessors for localization fields are moving to string extensions.")]
        public static AudioClip GetLocalizationAudioClip(string displayKey, Locale locale)
            => GetLocalization(displayKey, locale)?.AudioClip;

        /// <summary>
        /// Please use <see cref="SearchLocalesByName"/> and provide <see cref="LocaleSearchType.ConfiguredLocales"/> as
        /// a parameter.
        /// </summary>
        [Obsolete("Use \"SearchLocalesByName\" and pass in \"LocaleSearchType.ConfiguredLocales\" instead.")]
        public IEnumerable<Locale> SearchConfiguredLocalesByName(string searchByName)
        {
            return SearchLocalesByName(searchByName, LocaleSearchType.ConfiguredLocales);
        }
        
        /// <summary>
        /// Please use <see cref="SearchLocalesByName"/>.
        /// </summary>
        [Obsolete("Use \"SearchLocalesByName\".")]
        public IEnumerable<Locale> SearchAllLocalesByName(string searchByName)
        {
            return SearchLocalesByName(searchByName);
        }
        
        /// <summary>
        /// Please use <see cref="GetLocalesBySystemLanguage"/>.
        /// </summary>
        [Obsolete("Use \"GetLocalesBySystemLanguage\", TT is moving away from an excess of overloaded functions.")]
        public IEnumerable<Locale> GetAllLocales(SystemLanguage systemLanguage,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return GetLocalesBySystemLanguage(systemLanguage, localeSearchType);
        }
        
        /// <summary>
        /// Please use <see cref="GetLocaleBySystemLanguage"/>.
        /// </summary>
        [Obsolete("Use \"GetLocaleBySystemLanguage\", TT is moving away from an excess of overloaded functions.")]
        public Locale GetLocale(SystemLanguage systemLanguage, 
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return GetLocaleBySystemLanguage(systemLanguage, localeSearchType);
        }
        
        /// <summary>
        /// Please use <see cref="GetLocaleByIsoCode"/>.
        /// </summary>
        [Obsolete("Use \"GetLocaleByIsoCode\", TT is moving away from an excess of overloaded functions.")]
        public Locale GetLocale(ISO3166Alpha2 countryCode, ISO639Alpha2 languageCode, 
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return GetLocaleByIsoCode(countryCode, languageCode, localeSearchType);
        }
        
        /// <summary>
        /// Please use <see cref="GetLocalesByIsoCode"/>.
        /// </summary>
        [Obsolete("Use \"GetLocalesByIsoCode\", TT is moving away from an excess of overloaded functions.")]
        public IEnumerable<Locale> GetAllLocales(ISO3166Alpha2 countryCode, ISO639Alpha2 languageCode,
            LocaleSearchType localeSearchType = LocaleSearchType.AllLocales)
        {
            return GetLocalesByIsoCode(countryCode, languageCode, localeSearchType);
        }

        #endregion

        #region Virtual

        /// <summary>
        /// Compares two display names to see if they're congruent.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True if the left display name is congruent to the right display name.</returns>
        protected virtual bool CompareDisplayNames(string left, string right)
        {
            return String.Compare(left, right, StringComparison.OrdinalIgnoreCase) == 0;
        }
        
        /// <summary>
        /// Compares two native names to see if they're congruent.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True if the left native name is congruent to the right native name.</returns>
        protected virtual bool CompareNativeNames(string left, string right)
        {
            return String.Compare(left, right, StringComparison.InvariantCultureIgnoreCase) == 0;
        }

        /// <summary>
        /// Compares two <see cref="LocaleMetadata"/> objects to see if their <see cref="ISO639Alpha2"/> and
        /// <see cref="ISO3166Alpha2"/> codes match with one another.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns>True if the two objects have matching ISO codes.</returns>
        protected virtual bool CompareIsoCodes(LocaleMetadata left, LocaleMetadata right)
        {
            return left.ISOCountryCode == right.ISOCountryCode &&
                   left.ISOLanguageCode == right.ISOLanguageCode;
        }
        
        /// <summary>
        /// Searches all locales to find the "most similar" locale. This implementation may have distinct business
        /// requirements or differences per customer and thus is virtual/overridable. 
        /// </summary>
        /// <param name="localeMetadata">The locale metadata to use as similarity criteria.</param>
        /// <returns>
        /// The most similar locale in this <see cref="LocalizationManager"/> to the given locale metadata.
        /// </returns>
        protected virtual Locale GetSimilarLocale(LocaleMetadata localeMetadata)
        {
            var hasDisplayName = !string.IsNullOrWhiteSpace(localeMetadata.DisplayName);
            var hasNativeName = !string.IsNullOrWhiteSpace(localeMetadata.NativeName);
            var hasSystemLanguage = localeMetadata.UnitySystemLanguage != SystemLanguage.Unknown;
            var hasIsoCode = localeMetadata.ISOLanguageCode != ISO639Alpha2.NONE ||
                             localeMetadata.ISOCountryCode != ISO3166Alpha2.NONE;

            foreach (var locale in AllLocales)
            {
                if ((hasDisplayName && CompareDisplayNames(locale.Metadata.DisplayName, localeMetadata.DisplayName)) ||
                    (hasNativeName && CompareNativeNames(locale.Metadata.NativeName, localeMetadata.NativeName)) ||
                    (hasSystemLanguage && locale.Metadata.UnitySystemLanguage == localeMetadata.UnitySystemLanguage) ||
                    (hasIsoCode && CompareIsoCodes(locale.Metadata, localeMetadata)))
                {
                    return locale;
                }
            }

            return null;
        }

        /// <summary>
        /// Invoked after <see cref="Import"/> has completed its import process.
        /// </summary>
        /// <param name="localeLocalizationImport">The <see cref="LocaleLocalizationImport"/> which was imported.</param>
        protected virtual void OnImportedExternalLocalizations(LocaleLocalizationImport localeLocalizationImport) { }

        #endregion

        #region Supporting Types

        /// <summary>
        /// Used for determining what type of locales to search over.
        /// </summary>
        public enum LocaleSearchType
        {
            /// <summary>
            /// Search over both configured and additional locales.
            /// </summary>
            AllLocales,
            /// <summary>
            /// Search only the configured locales (locales purposefully configured in TT).
            /// </summary>
            ConfiguredLocales,
            /// <summary>
            /// Search only the additional locales (locales loaded externally from disk).
            /// </summary>
            ExternalLoadedLocales
        }

        #endregion

        #region Misc

        private Vector2 _localeButtonScrollArea;
        
        /// <summary>
        /// Draws a debug window within the scene if <see cref="_enableInGameDebugWindow"/> is enabled.
        /// </summary>
        /// <param name="windowId">The unique window ID pulled from <see cref="_inGameDebugWindowId"/></param>.
        private void DebugWindow(int windowId)
        {
            var richText = new GUIStyle() { richText = true};
            var richWrappingText = new GUIStyle() { wordWrap = true, richText = true };
            
            GUILayout.BeginHorizontal();
            
            // display current locale
            
            GUILayout.BeginVertical(GUI.skin.box);
            _localeButtonScrollArea = GUILayout.BeginScrollView(_localeButtonScrollArea, GUIStyle.none, GUI.skin.verticalScrollbar);
            GUILayout.Label("<b><color=#FFFFFFFF>Choose current locale:</color></b>", richText);
            foreach (var locale in AllLocales)
            {
                GUI.enabled = locale != _currentLocale;
                if (GUILayout.Button(new GUIContent(locale.Metadata.DisplayName, locale.Icon), GUILayout.Height(15)))
                {
                    CurrentLocale = locale;
                }
                GUI.enabled = true;
            }
            GUILayout.EndScrollView();
            GUILayout.EndVertical();
            
            // externally loaded locales

            GUILayout.BeginVertical();
            GUILayout.BeginVertical(GUI.skin.box);
            if (!_loadAdditionalLocalizationFiles)
            {
                GUILayout.Label(
                    "<color=#FFFFFFFF>This localization manager hasn't been configured to allow loading additional localization files.</color>",
                    richWrappingText);
            }
            GUI.enabled = _loadAdditionalLocalizationFiles;
            GUILayout.Label("<b><color=#FFFFFFFF>Externally loaded locales:</color></b>", richText);
            if (_externalLoadedLocalesCollection.Locales.Count > 0) 
            {
                foreach (var locale in _externalLoadedLocalesCollection.Locales)
                {
                    GUILayout.Label(new GUIContent(locale.Metadata.DisplayName, locale.Icon));
                }
            }
            else
            {
                GUILayout.Label("No externally loaded localizations");
            }
            GUI.enabled = true;
            GUILayout.EndVertical();
            GUILayout.BeginVertical(GUI.skin.box);
            GUI.enabled = _loadAdditionalLocalizationFiles;
            if (GUILayout.Button("Reload External Files"))
            {
                ReloadExternalLocalizations();
            }
            GUI.enabled = true;
            GUILayout.EndVertical();
            GUILayout.EndVertical();
            
            GUILayout.EndHorizontal();
        }

        #endregion
    }
}