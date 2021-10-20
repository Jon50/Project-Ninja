using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TongueTwister.Editor.Utilities;
using TongueTwister.Utilities;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor.Tools.Custom.CSVImporter
{
    /// <summary>
    /// CSV Importer will import two different types of CSV files where columns are display keys OR columns are locales.
    /// Depending on the selected mode, the first column should always be reserved for the latter. Imported data will
    /// be put into the currently selected LocalizationManager.
    /// </summary>
    public class CsvImporterTool : TongueTwisterTool
    {
        private const string DEBUG_TAG = "<b>[CSV-IMPORT]</b> - ";

        private const string LAST_USED_CSV_IMPORTER_SETTINGS =
            "TT:LastUsedCsvImporterSettings";

        private const string DEBUG_KEY =
            "TT:DebugCsvImporter";

        private CsvImporterSettings _csvImporterSettings;

        private CsvImporterSettingsEditor _csvImporterSettingsEditor;

        private bool _debug, _failedToCreateColumns;

        public override string Category => "Importers";

        public override string Title => "CSV Importer";

        public override string Description => "Imports translations from tabular (CSV) files.";

        public override string Version => "1.3";

        public override string ResourceIconPath => EditorGUIUtility.isProSkin ? "icons/importIcon" : "icons/importIconDark";

        public CsvImporterTool()
        {
            if(EditorPrefs.HasKey(DEBUG_KEY))
            {
                _debug = EditorPrefs.GetBool(DEBUG_KEY);
            }

            if(EditorPrefs.HasKey(LAST_USED_CSV_IMPORTER_SETTINGS))
            {
                var guid = EditorPrefs.GetString(LAST_USED_CSV_IMPORTER_SETTINGS);
                AssetUtility.TryGetAssetFromGuid(guid, out _csvImporterSettings);
                if(_csvImporterSettings)
                    CreateCsvImporterSettingsEditor();
            }
        }

        public override void DrawEditorUi()
        {
            var originalGuiColor = GUI.color;

            #region Debug / Logging

            var debug = EditorGUILayout.ToggleLeft("Enable Logging", _debug);
            if(debug != _debug)
            {
                EditorPrefs.SetBool(DEBUG_KEY, debug);
                _debug = debug;
            }

            DrawHorizontalBreak();

            #endregion

            #region Settings Select

            GUILayout.BeginHorizontal();
            GUILayout.Label("Step 1:", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            var csvImporterSettings =
                EditorGUILayout.ObjectField(
                        new GUIContent(
                            "Select Importer Settings",
                            "All settings and configurations made below will be saved to a CSV Importer Settings object to allow for easy swapping between different files/settings."),
                        _csvImporterSettings,
                        typeof(CsvImporterSettings),
                        false)
                    as CsvImporterSettings;
            GUILayout.EndHorizontal();

            if(csvImporterSettings != _csvImporterSettings)
            {
                _csvImporterSettings = csvImporterSettings;
                CreateCsvImporterSettingsEditor();
                EditorPrefs.SetString(LAST_USED_CSV_IMPORTER_SETTINGS, AssetUtility.GetAssetGuid(_csvImporterSettings));
            }

            if(!_csvImporterSettings || _csvImporterSettings == null)
            {
                DrawRichHorizontalCenteredLabel("<color=#FF0000FF>Please select a CSV Importer Settings object</color>");

                if(DrawHorizontalCenteredButton(new GUIContent("Create New")))
                {
                    _csvImporterSettings = CreateNewCsvImporterSettingsAsset();
                    CreateCsvImporterSettingsEditor();
                    EditorPrefs.SetString(LAST_USED_CSV_IMPORTER_SETTINGS, AssetUtility.GetAssetGuid(_csvImporterSettings));
                }

                return;
            }

            DrawHorizontalBreak();

            #endregion

            #region File Select

            GUILayout.BeginHorizontal();
            GUILayout.Label("Step 2:", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            var file =
                EditorGUILayout.TextField(
                    new GUIContent("Select CSV File", "The CSV file that'll be used to import translations."),
                    _csvImporterSettingsEditor.File,
                    GUILayout.ExpandWidth(true));
            if(GUILayout.Button("Browse", GUILayout.ExpandWidth(false)))
            {
                var result = EditorUtility.OpenFilePanel(
                    "Select Import File",
                    Application.dataPath,
                    "csv");

                if(!string.IsNullOrWhiteSpace(result) && File.Exists(result))
                {
                    file = result;
                }
            }

            if(GUILayout.Button(
                new GUIContent("Open in Editor", "Opens the file."),
                GUILayout.ExpandWidth(false)))
            {
                EditorUtility.OpenWithDefaultApp(file);
            }

            if(file != _csvImporterSettingsEditor.File)
            {
                _csvImporterSettingsEditor.File = file;
                GUI.FocusControl(null);
            }

            GUILayout.EndHorizontal();

            if(string.IsNullOrWhiteSpace(_csvImporterSettingsEditor.File) ||
                !_csvImporterSettingsEditor.File.ToLower().EndsWith(".csv") ||
                !File.Exists(_csvImporterSettingsEditor.File))
            {
                DrawRichHorizontalCenteredLabel("<color=#FF0000FF>Please select a valid CSV file</color>");
                return;
            }

            DrawHorizontalBreak();

            #endregion

            #region Select Delimiter Mode

            GUILayout.BeginHorizontal();
            GUILayout.Label("Step 3:", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            GUILayout.Label("Select Delimiter Method");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(15);

            var delimiterMode =
                (DelimiterModeType)GUILayout.SelectionGrid(
                    (int)_csvImporterSettingsEditor.DelimiterMode,
                    DelimiterModeTypeLabels,
                    4);

            if(delimiterMode != _csvImporterSettingsEditor.DelimiterMode)
            {
                _csvImporterSettingsEditor.DelimiterMode = delimiterMode;
            }

            if(delimiterMode == DelimiterModeType.Custom)
            {
                var customDelimiter = EditorGUILayout.TextField(
                    new GUIContent(
                        "Custom Delimiter",
                        "The custom delimiter which is used to separate column entries in the CSV file."),
                    _csvImporterSettingsEditor.CustomCsvDelimiter);

                if(customDelimiter != _csvImporterSettingsEditor.CustomCsvDelimiter)
                {
                    _csvImporterSettingsEditor.CustomCsvDelimiter = customDelimiter;
                }
            }

            if(delimiterMode == DelimiterModeType.Custom &&
                string.IsNullOrWhiteSpace(_csvImporterSettingsEditor.CustomCsvDelimiter))
            {
                DrawRichHorizontalCenteredLabel("<color=#FF0000FF>Custom delimiter cannot be an empty or null string.</color>");
                return;
            }

            DrawHorizontalBreak();

            #endregion

            #region Import Settings

            var importSettingsValid = true;

            GUILayout.BeginHorizontal();
            GUILayout.Label("Step 4:", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            GUILayout.Label("Configure import settings");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(15);

            var fileType = (CsvFileType)EditorGUILayout.EnumPopup(
                new GUIContent(
                    "Column Structure",
                    "Set how the CSV file is structured in terms of columns and rows."),
                _csvImporterSettingsEditor.FileType);

            if(fileType == CsvFileType.None)
            {
                var richWordWrappedStyle = new GUIStyle() { richText = true, wordWrap = true };
                GUILayout.Label(
                    "<color=#FF0000FF>Choose a CSV column structure type to continue.</color>",
                    richWordWrappedStyle);
            }

            if(fileType != _csvImporterSettingsEditor.FileType)
            {
                _csvImporterSettingsEditor.FileType = fileType;
            }

            if(fileType == CsvFileType.None)
                return;

            var localeImportType = (LocaleImportType)EditorGUILayout.EnumPopup(
                new GUIContent(
                    "Locale Import Type",
                    "Determines how locales are imported from the CSV file."),
                _csvImporterSettingsEditor.LocaleImportType);

            if(localeImportType == LocaleImportType.None)
            {
                GUILayout.Label(
                    "Choose a locale import type to continue.",
                    EditorStyles.wordWrappedLabel);
            }

            if(localeImportType != _csvImporterSettingsEditor.LocaleImportType)
            {
                _csvImporterSettingsEditor.LocaleImportType = localeImportType;
            }

            if(localeImportType == LocaleImportType.None)
                return;

            GUILayout.Space(15);

            GUILayout.Label("Extra Settings", EditorStyles.boldLabel);

            var groupDelimiter =
                EditorGUILayout.TextField(
                    new GUIContent(
                        "Group Delimiter",
                        "The character or phrase which separates groups in imported display keys."),
                    _csvImporterSettingsEditor.GroupDelimiter);

            if(string.IsNullOrEmpty(groupDelimiter))
            {
                DrawRichHorizontalCenteredLabel("<color=#FF0000FF>Group delimiter cannot be empty.</color>");
                importSettingsValid = false;
            }

            if(groupDelimiter != _csvImporterSettingsEditor.GroupDelimiter)
            {
                _csvImporterSettingsEditor.GroupDelimiter = groupDelimiter;
            }

            var createNewLocales =
                EditorGUILayout.ToggleLeft(
                    new GUIContent(
                        "Auto create imported locales if they don't exist",
                        "When an imported locale does not match any of the configured locales, a new locale will be created. Otherwise, it will be ignored."),
                    _csvImporterSettingsEditor.CreateNewLocales);

            if(createNewLocales != _csvImporterSettingsEditor.CreateNewLocales)
            {
                _csvImporterSettingsEditor.CreateNewLocales = createNewLocales;
            }

            GUILayout.Space(15);
            DrawPreviewCsvColumns(fileType, localeImportType);

            DrawHorizontalBreak();

            if(!importSettingsValid)
                return;

            #endregion

            #region Begin Import

            GUILayout.BeginHorizontal();
            GUILayout.Label("Step 5:", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            GUILayout.Label("Import all localization data");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(15);

            if(GUILayout.Button("Begin Import"))
            {
                EditorUtility.DisplayProgressBar("Importing", "Spinning up", 0.0f);
                try
                {
                    Import();
                }
                catch(Exception exception)
                {
                    EditorUtility.DisplayDialog("Error",
                        $"Failed to import the CSV file. Reason:\n\n{exception.Message}", "Close");
                }
                EditorUtility.ClearProgressBar();
            }

            #endregion
        }

        private void ValidateCsvImporterSettings()
        {
            if(_csvImporterSettingsEditor == null || _csvImporterSettings == null)
            {
                throw new Exception("No CSV Importer Settings have been chosen.");
            }

            if(string.IsNullOrWhiteSpace(_csvImporterSettingsEditor.File))
            {
                throw new Exception("CSV file is empty or null.");
            }

            if(_csvImporterSettingsEditor.FileType == CsvFileType.None)
            {
                throw new Exception("CSV file type is \"None\", please select a valid file type.");
            }

            if(_csvImporterSettingsEditor.LocaleImportType == LocaleImportType.None)
            {
                throw new Exception("Locale import type is \"None\", please select a locale import type.");
            }

            if(_csvImporterSettingsEditor.DelimiterMode == DelimiterModeType.Custom &&
                string.IsNullOrWhiteSpace(_csvImporterSettingsEditor.CustomCsvDelimiter))
            {
                throw new Exception("Delimiter mode is set to \"Custom\" but the custom delimiter value is empty or null.");
            }

            if(string.IsNullOrWhiteSpace(_csvImporterSettingsEditor.GroupDelimiter))
            {
                throw new Exception("Group delimiter is empty or null.");
            }
        }

        private void Import()
        {
            try
            {
                if(_debug)
                    Debug.Log($"{DEBUG_TAG}Starting CSV import");

                ValidateCsvImporterSettings();

                if(_debug)
                    Debug.Log($"{DEBUG_TAG}Validated CSV Importer Settings");

                var file = _csvImporterSettingsEditor.File;
                var csvDelimiter = GetDelimiter(_csvImporterSettings);
                var groupDelimiter = _csvImporterSettingsEditor.GroupDelimiter;
                var csvFileType = _csvImporterSettingsEditor.FileType;
                var localeImportType = _csvImporterSettingsEditor.LocaleImportType;
                var createNewLocales = _csvImporterSettingsEditor.CreateNewLocales;

                if(_debug)
                    Debug.Log($"{DEBUG_TAG}Settings...\n" +
                              $"file: {file}\n" +
                              $"csvDelimiter: {csvDelimiter}\n" +
                              $"groupDelimiter: {groupDelimiter}\n" +
                              $"csvFileType: {csvFileType}\n" +
                              $"localeImportType: {localeImportType}\n" +
                              $"createNewLocales: {createNewLocales}\n");

                var dataRows =
                    File.ReadAllLines(file)
                        .Select(line => SplitCsvRow(line, csvDelimiter))
                        .ToList();

                if(_debug)
                    Debug.Log($"{DEBUG_TAG}Found {dataRows.Count} rows.");

                var columns = dataRows[0];
                dataRows.RemoveAt(0);

                if(_debug)
                    Debug.Log($"{DEBUG_TAG}Found {columns.Count} columns.");

                var localizationDictionary = new LocalizationDictionary();
                var successfullyImportedTranslations = 0;

                if(csvFileType == CsvFileType.LocaleColumns)
                {
                    var displayKeys = new List<string>();
                    for(int i = 0; i < dataRows.Count; i++)
                    {
                        displayKeys.Add(dataRows[i][0]);
                    }
                    for(int column = 1; column < columns.Count; column++)
                    {
                        var columnName = columns[column];

                        if(_debug)
                            Debug.Log($"{DEBUG_TAG}Evaluating column named \"{columnName}\"");

                        Locale locale = null;

                        switch(localeImportType)
                        {
                            case LocaleImportType.DisplayName:
                                locale = GetOrCreateLocaleFromName(columnName, createNewLocales);
                                break;
                            case LocaleImportType.LocalizationCode:
                                ISOUtility.ParseCodes(columnName, out var countryCode, out var languageCode);
                                locale = GetOrCreateLocaleFromCodes(countryCode, languageCode, createNewLocales);
                                break;
                            default:
                                continue;
                        }

                        for(int row = 0; row < dataRows.Count; row++)
                        {
                            var displayKey = displayKeys[row].Replace(groupDelimiter, ".");
                            var translation = dataRows[row][column];

                            EditorUtility.DisplayProgressBar(
                                "Importing",
                                translation,
                                (float)column / columns.Count);

                            if(!localizationDictionary.ContainsKey(displayKey))
                            {
                                localizationDictionary.Set(displayKey, new DisplayKeyLocalizationSet(displayKey));
                            }

                            localizationDictionary[displayKey][locale] = new Localization()
                            {
                                Locale = locale,
                                Text = translation
                            };

                            successfullyImportedTranslations++;

                            if(_debug)
                                Debug.Log($"{DEBUG_TAG}Imported \"{displayKey}\":\"{translation}\"");
                        }
                    }
                }
                else if(csvFileType == CsvFileType.DisplayKeyColumns)
                {
                    var locales = new List<Locale>();
                    for(int i = 0; i < dataRows.Count; i++)
                    {
                        var rowValue = dataRows[i][0];
                        Locale locale = null;

                        switch(localeImportType)
                        {
                            case LocaleImportType.DisplayName:
                                locale = GetOrCreateLocaleFromName(rowValue, createNewLocales);
                                break;
                            case LocaleImportType.LocalizationCode:
                                ISOUtility.ParseCodes(rowValue, out var countryCode, out var languageCode);
                                locale = GetOrCreateLocaleFromCodes(countryCode, languageCode, createNewLocales);
                                break;
                            default:
                                continue;
                        }

                        if(locale == null)
                        {
                            continue;
                        }

                        locales.Add(locale);
                    }
                    for(int column = 1; column < columns.Count; column++)
                    {
                        var displayKey = columns[column];

                        if(_debug)
                            Debug.Log($"{DEBUG_TAG}Evaluating column named \"{displayKey}\"");

                        for(int row = 0; row < dataRows.Count; row++)
                        {
                            var locale = locales[row];
                            var translation = dataRows[row][column];

                            EditorUtility.DisplayProgressBar(
                                "Importing",
                                translation,
                                (float)column / columns.Count);

                            if(!localizationDictionary.ContainsKey(displayKey))
                            {
                                localizationDictionary.Set(displayKey, new DisplayKeyLocalizationSet(displayKey));
                            }

                            localizationDictionary[displayKey][locale] = new Localization()
                            {
                                Locale = locale,
                                Text = translation
                            };

                            successfullyImportedTranslations++;

                            if(_debug)
                                Debug.Log($"{DEBUG_TAG}Imported \"{displayKey}\":\"{translation}\"");
                        }
                    }
                }
                else
                {
                    throw new Exception($"CSV File Type not supported: {csvFileType}");
                }

                MergeOverwritePreCheck(localizationDictionary);

                TongueTwisterWindow.CurrentWindow.MergeLocalizationDictionary(localizationDictionary);

                Debug.Log($"{DEBUG_TAG}<color=#00FF00FF>SUCCESS</color> - imported {successfullyImportedTranslations} translations.");
            }
            catch(Exception exception)
            {
                Debug.Log($"{DEBUG_TAG}<color=#FF0000FF>FAILED</color> - {exception.Message}");
                throw;
            }
        }

        private void MergeOverwritePreCheck( LocalizationDictionary localizationDictionary )
        {
            var existingDictionary = TongueTwisterWindow.LocalizationDictionary;

            foreach(var displayKey in localizationDictionary.Keys)
            {
                if(existingDictionary.ContainsKey(displayKey))
                {
                    foreach(var newLocalization in localizationDictionary[displayKey].GetAllLocalizations())
                    {
                        if(existingDictionary[displayKey].HasLocalization(newLocalization.Locale))
                        {
                            Debug.Log($"{DEBUG_TAG}<color=#FFFF00FF>WARNING</color> - Adding possible duplicate localization for {displayKey} and locale {newLocalization.Locale?.Metadata?.DisplayName ?? ("UNNAMED")}");
                        }
                    }
                }
            }
        }

        private void CreateCsvImporterSettingsEditor()
        {
            _csvImporterSettingsEditor =
                UnityEditor.Editor.CreateEditor(
                        _csvImporterSettings,
                        typeof(CsvImporterSettingsEditor))
                    as CsvImporterSettingsEditor;
        }

        private Locale GetLocaleFromCurrentTTW( string name, ISO3166Alpha2? countryCode, ISO639Alpha2? languageCode )
        {
            if(!string.IsNullOrWhiteSpace(name))
            {
                var nameSearchResults = TongueTwisterWindow.CurrentWindow.LocalizationManager.SearchLocalesByName(name).ToArray();

                if(nameSearchResults.Length == 1)
                {
                    return nameSearchResults[0];
                }

                if(nameSearchResults.Length > 1)
                {
                    Debug.Log($"{DEBUG_TAG}Found multiple locales matching {name}, cannot determine which to use...");
                }
            }

            if(countryCode == null || languageCode == null)
            {
                return null;
            }

            var codeResults = TongueTwisterWindow.CurrentWindow.LocalizationManager.GetLocalesByIsoCode((ISO3166Alpha2)countryCode, (ISO639Alpha2)languageCode).ToArray();

            if(codeResults.Length == 1)
            {
                return codeResults[0];
            }

            if(codeResults.Length > 1)
            {
                Debug.Log($"{DEBUG_TAG}Found multiple locales matching {countryCode} and {languageCode}, cannot determine which to use...");
            }

            return null;
        }

        private Locale GetOrCreateLocaleFromName( string name, bool createNewLocales )
        {
            var locale = GetLocaleFromCurrentTTW(name, null, null);

            if(locale != null)
            {
                if(_debug)
                {
                    Debug.Log($"{DEBUG_TAG}Successfully found matching locale named \"{name}\", file named \"{locale.Metadata.DisplayName}\"");
                }

                return locale;
            }

            if(createNewLocales)
            {
                if(_debug)
                {
                    Debug.Log($"{DEBUG_TAG}Attempting to create new locale named \"{name}\"");
                }

                try
                {
                    locale = new Locale(new LocaleMetadata()
                    {
                        DisplayName = name
                    });

                    TongueTwisterWindow.CurrentWindow.AddLocaleToConfiguredLocales(locale);

                    if(_debug)
                    {
                        Debug.Log($"{DEBUG_TAG}Created new locale named \"{name}\".");
                    }

                    return locale;
                }
                catch(Exception exception)
                {
                    Debug.LogError($"{DEBUG_TAG}Skipping column. Failed to create new locale from \"{name}\", skipping. Exception: {exception.Message}");
                    return null;
                }
            }

            if(_debug)
            {
                Debug.Log($"{DEBUG_TAG}Skipping column, \"createNewLocales\" is disabled and no locale could be found with name \"{name}\"");
            }

            return null;
        }

        private Locale GetOrCreateLocaleFromCodes( ISO3166Alpha2 countryCode, ISO639Alpha2 languageCode, bool createNewLocales )
        {
            var codeString = TongueTwisterStringUtility.CreateCodeString(countryCode, languageCode);
            var locale = GetLocaleFromCurrentTTW(null, countryCode, languageCode);

            if(locale != null)
            {
                if(_debug)
                {
                    Debug.Log($"{DEBUG_TAG}Successfully found matching locale with code \"{codeString}\", file named \"{TongueTwisterStringUtility.CreateCodeString(locale.Metadata.ISOCountryCode, locale.Metadata.ISOLanguageCode)}\"");
                }

                return locale;
            }

            if(createNewLocales)
            {
                if(_debug)
                {
                    Debug.Log($"{DEBUG_TAG}Attempting to create new locale with code \"{codeString}\"");
                }

                try
                {
                    locale = new Locale(new LocaleMetadata()
                    {
                        ISOCountryCode = countryCode,
                        ISOLanguageCode = languageCode
                    });

                    TongueTwisterWindow.CurrentWindow.AddLocaleToConfiguredLocales(locale);

                    if(_debug)
                    {
                        Debug.Log($"{DEBUG_TAG}Created new locale with code \"{codeString}\". The locale has no name, it is advised to configure one.");
                    }

                    return locale;
                }
                catch(Exception exception)
                {
                    Debug.LogError($"{DEBUG_TAG}Skipping column. Failed to create new locale with code \"{codeString}\", skipping. Exception: {exception.Message}");
                    return null;
                }
            }

            if(_debug)
            {
                Debug.Log($"{DEBUG_TAG}Skipping column, \"createNewLocales\" is disabled and no locale could be found with code \"{codeString}\"");
            }

            return null;
        }

        private List<string> SplitCsvRow( string row, string delimiter )
        {
            var results = new List<string>();
            var formattedRow = row.Trim();
            var rowLength = formattedRow.Length;
            var inQuotationBlock = false;
            var currentData = "";

            for(int i = 0; i < rowLength; i++)
            {
                var currentChar = formattedRow[i];
                var nextChar = (i < rowLength - 1) ? formattedRow[i + 1] : '\0';

                if(currentChar == '"')
                {
                    if(inQuotationBlock)
                    {
                        if(nextChar == '"')
                        {
                            currentData += currentChar;
                            i++;
                            continue;
                        }

                        if(nextChar == '\0' || nextChar == '\n')
                        {
                            results.Add(currentData);
                            break;
                        }

                        inQuotationBlock = false;
                        continue;
                    }

                    inQuotationBlock = true;
                    continue;
                }

                if(!inQuotationBlock && CheckForDelimiterAtPosition(formattedRow, i, rowLength, delimiter))
                {
                    results.Add(currentData);
                    currentData = "";
                    i += delimiter.Length - 1;
                    continue;
                }

                currentData += currentChar;

                if(nextChar == '\0' || nextChar == '\n')
                {
                    results.Add(currentData);
                    break;
                }
            }

            for(int i = 0; i < results.Count; i++)
            {
                var result = results[i];
                result = result.Trim();
                results[i] = result;
            }

            return results;
        }

        private bool CheckForDelimiterAtPosition( string row, int startPosition, int rowLength, string delimiter )
        {
            var i = 0;
            var delimiterLength = delimiter.Length;
            while(i + startPosition < rowLength && i < delimiterLength)
            {
                if(i + startPosition > rowLength - 1 || row[i + startPosition] != delimiter[i])
                {
                    return false;
                }

                i++;
            }
            return true;
        }

        private CsvImporterSettings CreateNewCsvImporterSettingsAsset()
        {
            var newCsvImporterSettingsNum = 0;
            var baseName = "CSV Importer Settings";
            while(true)
            {
                var name = newCsvImporterSettingsNum == 0
                    ? $"{baseName}.asset"
                    : $"{baseName} {newCsvImporterSettingsNum}.asset";
                var path = $"Assets/{name}";

                if(string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path)))
                {
                    var newCsvImporterSettings = ScriptableObject.CreateInstance<CsvImporterSettings>();
                    AssetDatabase.CreateAsset(newCsvImporterSettings, path);
                    AssetDatabase.SaveAssets();
                    return newCsvImporterSettings;
                }

                newCsvImporterSettingsNum++;
            }
        }

        private void DrawRichHorizontalCenteredLabel( string label )
        {
            var richStyle = new GUIStyle() { richText = true };
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label(label, richStyle);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }

        private bool DrawHorizontalCenteredButton( GUIContent guiContent )
        {
            var clicked = false;
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            clicked = GUILayout.Button(guiContent);
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            return clicked;
        }

        private void DrawHorizontalBreak( float marginTop = 15.0f, float marginBottom = 15.0f, float marginLeft = 0.0f,
            float marginRight = 0.0f )
            => DrawHorizontalBreak(2.0f, marginTop, marginBottom, marginLeft, marginRight);

        private void DrawHorizontalBreak( float height, float marginTop = 0.0f, float marginBottom = 0.0f,
            float marginLeft = 0.0f, float marginRight = 0.0f )
        {
            var hasProLicense = EditorGUIUtility.isProSkin;
            var color = Color.black;
            color.a = hasProLicense ? 0.9f : 0.1f;

            GUILayout.BeginVertical();
            GUILayout.Space(marginTop);
            GUILayout.BeginHorizontal();
            GUILayout.Space(marginLeft);
            var originalColor = GUI.color;
            GUI.color = color;
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(height));
            GUI.color = originalColor;
            GUILayout.Space(marginRight);
            GUILayout.EndHorizontal();
            GUILayout.Space(marginBottom);
            GUILayout.EndVertical();
        }

        /// <summary>
        /// Draws a block of text for previewing what a CSV file might look like with the given settings.
        /// </summary>
        /// <param name="csvFileType"></param>
        /// <param name="localeImportType"></param>
        private void DrawPreviewCsvColumns( CsvFileType csvFileType, LocaleImportType localeImportType )
        {
            GUILayout.Label(
                new GUIContent(
                    "Preview",
                    "Your CSV file should look something like this."),
                EditorStyles.boldLabel);
            GUILayout.BeginHorizontal(GUI.skin.box);
            for(int i = 0; i < 4; i++)
            {
                if(csvFileType == CsvFileType.LocaleColumns)
                {
                    GUILayout.BeginVertical();
                    if(i == 0)
                    {
                        GUILayout.Label("\"Display Key\",", EditorStyles.miniBoldLabel);
                        for(int j = 0; j < 3; j++)
                        {
                            GUILayout.Label(GetDisplayKeyForCsvPreview(j), EditorStyles.miniLabel);
                        }
                    }
                    else
                    {
                        GUILayout.Label(GetLocaleForCsvPreview(i - 1, localeImportType), EditorStyles.miniBoldLabel);
                        for(int j = 0; j < 3; j++)
                        {
                            GUILayout.Label(GetTranslationForCsvPreview(i - 1, j), EditorStyles.miniLabel);
                        }
                    }
                    GUILayout.EndVertical();
                }
                else
                {
                    GUILayout.BeginVertical();
                    if(i == 0)
                    {
                        GUILayout.Label("\"Locale\",", EditorStyles.miniBoldLabel);
                        for(int j = 0; j < 3; j++)
                        {
                            GUILayout.Label(GetLocaleForCsvPreview(j, localeImportType), EditorStyles.miniLabel);
                        }
                    }
                    else
                    {
                        GUILayout.Label(GetDisplayKeyForCsvPreview(i - 1), EditorStyles.miniBoldLabel);
                        for(int j = 0; j < 3; j++)
                        {
                            GUILayout.Label(GetTranslationForCsvPreview(i - 1, j), EditorStyles.miniLabel);
                        }
                    }
                    GUILayout.EndVertical();
                }
            }
            GUILayout.EndHorizontal();
        }

        /// <summary>
        /// Used solely for getting the CSV preview text, this does nothing else.
        /// </summary>
        /// <param name="displayKey"></param>
        /// <returns></returns>
        private string GetDisplayKeyForCsvPreview( int displayKey )
        {
            var groupDelimiter = _csvImporterSettings.groupDelimiter;
            switch(displayKey)
            {
                case 0:
                    return $"\"Greetings{groupDelimiter}Greetings1\",";
                case 1:
                    return "\"Greetings2\",";
                case 2:
                    return $"\"Departures{groupDelimiter}Byes{groupDelimiter}Bye1\",";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Used solely for getting the CSV preview text, this does nothing else.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="localeImportType"></param>
        /// <returns></returns>
        private string GetLocaleForCsvPreview( int locale, LocaleImportType localeImportType )
        {
            switch(locale)
            {
                case 0:
                    return localeImportType == LocaleImportType.LocalizationCode ? "\"en-us\"," : "\"American English\",";
                case 1:
                    return localeImportType == LocaleImportType.LocalizationCode ? "\"es-mx\"," : "\"Mexican Spanish\",";
                case 2:
                    return localeImportType == LocaleImportType.LocalizationCode ? "\"de-de\"," : "\"German\",";
                default:
                    return null;
            }
        }

        /// <summary>
        /// Used solely for getting the CSV preview text, this does nothing else.
        /// </summary>
        /// <param name="locale"></param>
        /// <param name="displayKey"></param>
        /// <returns></returns>
        private string GetTranslationForCsvPreview( int locale, int displayKey )
        {
            switch(locale)
            {
                case 0: // english
                    switch(displayKey)
                    {
                        case 0: // greeting 1
                            return "\"Hello\",";
                        case 1: // greeting 2
                            return "\"Hi\",";
                        case 2: // departure 1
                            return "\"Bye\",";
                        default:
                            return null;
                    }
                case 1: // spanish
                    switch(displayKey)
                    {
                        case 0: // greeting 1
                            return "\"Hola\",";
                        case 1: // greeting 2
                            return "\"Bienvenido\",";
                        case 2: // departure 1
                            return "\"Adios\",";
                        default:
                            return null;
                    }
                case 2: // german
                    switch(displayKey)
                    {
                        case 0: // greeting 1
                            return "\"Hallo\",";
                        case 1: // greeting 2
                            return "\"Guten Tag\",";
                        case 2: // departure 1
                            return "\"Auf wiedersehen\",";
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        private static readonly string[] DelimiterModeTypeLabels = new[] { "Comma", "Tab", "Space", "Custom" };

        private static string GetDelimiter( CsvImporterSettings csvImporterSettings )
        {
            switch(csvImporterSettings.delimiterMode)
            {
                case DelimiterModeType.Comma:
                    return ",";
                case DelimiterModeType.Space:
                    return " ";
                case DelimiterModeType.Tab:
                    return "\t";
                case DelimiterModeType.Custom:
                    return csvImporterSettings.customCsvDelimiter;
                default:
                    return "";
            }
        }
    }

    public enum DelimiterModeType
    {
        Comma,
        Tab,
        Space,
        Custom
    }
}