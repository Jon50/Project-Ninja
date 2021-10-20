using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor.Tools.Custom
{
    /// <summary>
    /// Imports Serialized Localization Sets from JSON files. These SLS files are the standard file approach for
    /// transporting TongueTwister data through the file system.
    /// </summary>
    public class SerializedLocalizationSetImporter : TongueTwisterTool
    {
        private const string IMPORT_DEBUG_TAG = 
            "<b>[SLS-IMPORT]</b> - ";

        private const string LAST_USED_IMPORT_PATH_PREF_NAME = 
            "TT:LastUsedSLSImporterPath";
        
        private const string DEBUG_PREF_NAME =
            "TT:DebugSLSImporter";

        private bool _debug;

        private string _importPath;

        public override string Category => "Importers";

        public override string Title => "Serialized Localization Set Importer";

        public override string Description => "Imports serialized localization sets from JSON files.";

        public override string Version => "1.1";
        
        public override string ResourceIconPath => EditorGUIUtility.isProSkin ? "icons/importIcon" : "icons/importIconDark";

        public SerializedLocalizationSetImporter()
        {
            if (EditorPrefs.HasKey(DEBUG_PREF_NAME))
            {
                _debug = EditorPrefs.GetBool(DEBUG_PREF_NAME);
            }

            if (EditorPrefs.HasKey(LAST_USED_IMPORT_PATH_PREF_NAME))
            {
                _importPath = EditorPrefs.GetString(LAST_USED_IMPORT_PATH_PREF_NAME);
            }
        }
        
        public override void DrawEditorUi()
        {
            #region Debug / Logging
            
            var debug = EditorGUILayout.ToggleLeft("Enable Logging", _debug);
            if (debug != _debug)
            {
                EditorPrefs.SetBool(DEBUG_PREF_NAME, debug);
                _debug = debug;
            }
            
            DrawHorizontalBreak();
            
            #endregion
            
            #region Folder Select
            
            GUILayout.BeginHorizontal();
            GUILayout.Label("Step 1:", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            var importPath = EditorGUILayout.TextField(
                new GUIContent(
                    "Select Import Path",
                    "The folder at the designated path should contain one or more serialized localization sets."),
                _importPath);
            if (GUILayout.Button("Browse", GUILayout.ExpandWidth(false)))
            {
                var result = EditorUtility.OpenFolderPanel(
                    "Select Import Folder", 
                    Application.dataPath, 
                    importPath);
                
                if (!string.IsNullOrWhiteSpace(result) && Directory.Exists(result))
                {
                    importPath = result;
                    GUI.FocusControl(null);
                }
            }
            GUILayout.EndHorizontal();

            if (importPath != _importPath)
            {
                _importPath = importPath;
                EditorPrefs.SetString(LAST_USED_IMPORT_PATH_PREF_NAME, _importPath);
            }

            if (!Directory.Exists(_importPath))
            {
                GUILayout.Label("Please select a valid path to continue.");
                return;
            }
            
            DrawHorizontalBreak();
            
            #endregion

            #region Import

            GUILayout.BeginHorizontal();
            GUILayout.Label("Step 2:", EditorStyles.boldLabel, GUILayout.ExpandWidth(false));
            GUILayout.Space(15);
            GUILayout.Label("Import all serialized localization set data");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(15);

            if (GUILayout.Button("Begin Import"))
            {
                EditorUtility.DisplayProgressBar("Importing", "Spinning up...", 0.0f);
                try
                {
                    Import();
                }
                catch (Exception exception)
                {
                    EditorUtility.DisplayDialog("Error",
                        $"Import failed. Reason:\n\n{exception.Message}", "Close");
                }
                EditorUtility.ClearProgressBar();
            }

            #endregion
        }
        
        private void DrawHorizontalBreak(float marginTop = 15.0f, float marginBottom = 15.0f, float marginLeft = 0.0f,
            float marginRight = 0.0f)
            => DrawHorizontalBreak(2.0f, marginTop, marginBottom, marginLeft, marginRight); 
        
        private void DrawHorizontalBreak(float height, float marginTop = 0.0f, float marginBottom = 0.0f, 
            float marginLeft = 0.0f, float marginRight = 0.0f)
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

        private void Import()
        {
            try
            {
                if (_debug) Debug.Log($"{IMPORT_DEBUG_TAG}Starting Serialized Localization Set import");

                var files = Directory.GetFiles(_importPath).Where(file => file.ToLower().EndsWith(".json")).ToList();

                if (files.Count == 0)
                {
                    throw new Exception($"Found 0 JSON files at import path: {_importPath}");
                }
                
                var successfullyImportedLocales = 0;
                var localizationDictionary = new LocalizationDictionary();
                var localizationManagerEditor = TongueTwisterWindow.CurrentWindow.LocalizationManagerEditor;
                var configuredLocales = localizationManagerEditor.ConfiguredLocales;
                var newLocales = new List<Locale>();

                foreach (var file in files)
                {
                    var fileName = file.Replace("\\", "/").Split('/').Last();
                    
                    EditorUtility.DisplayProgressBar(
                        "Importing", 
                        fileName, 
                        (float)files.IndexOf(file) / files.Count);

                    if (_debug) Debug.Log($"{IMPORT_DEBUG_TAG}Importing file: {fileName}");

                    try
                    {
                        // even though the "out" locale won't have a valid ID (just based on how this function works),
                        // the localizations that are imported from the file will still retain the object reference
                        // that was made. Thus, the locale ID is able to be determined externally from the function.
                        // However, if a custom implementation exists for "CheckImportedLocaleAlreadyExists()", the
                        // system will automatically replace all occurrences of the newly created locale with the 
                        // provided existing locale within the dictionary.
                        
                        localizationDictionary.AddLocalizationSetFromJsonFile(file, out Locale locale);
                        
                        if (!CheckImportedLocaleAlreadyExists(
                            locale, 
                            configuredLocales, 
                            out Locale existingLocale))
                        {
                            newLocales.Add(locale);
                            successfullyImportedLocales++;
                        }
                        else
                        {
                            localizationDictionary.ReplaceLocale(locale, existingLocale);
                        }
                    }
                    catch (Exception exception)
                    {
                        if (_debug) Debug.Log($"{IMPORT_DEBUG_TAG}Failed to import file {fileName}, reason: {exception.Message}");
                    }
                }
                
                var existingLocales = localizationManagerEditor.ConfiguredLocales;
                existingLocales.AddRange(newLocales);
                localizationManagerEditor.ConfiguredLocales = existingLocales;

                TongueTwisterWindow.CurrentWindow.MergeLocalizationDictionary(localizationDictionary);

                Debug.Log($"{IMPORT_DEBUG_TAG}<color=#00FF00FF>SUCCESS</color> - imported {successfullyImportedLocales} serialized locales.");

            }
            catch (Exception exception)
            {
                Debug.Log($"{IMPORT_DEBUG_TAG}<color=#FF0000FF>FAILED</color> - {exception.Message}");
                throw;
            }
        }

        /// <summary>
        /// Checks to see if the new locale already exists amongst the configured locales. Requires a custom
        /// implementation, the default TT implementation always returns false and assumes the imported locale is
        /// entirely new. In which case, the system will give it a new ID.
        /// </summary>
        /// <param name="newLocale">The deserialized locale from a serialization file.</param>
        /// <param name="configuredLocales">The list of existing locales.</param>
        /// <param name="matchingLocale">The existing locale that matches the new locale closest.</param>
        /// <returns>Whether or not the locale already exists within the configured locales list.</returns>
        protected virtual bool CheckImportedLocaleAlreadyExists(Locale newLocale, List<Locale> configuredLocales, out Locale matchingLocale)
        {
            // currently there are no plans to implement this. However, suggestions for custom implementations would
            // include comparing the names of locales on a "fuzzy search" basis (or maybe even a strict search). 
            
            matchingLocale = null;
            return false;
        }
    }
}