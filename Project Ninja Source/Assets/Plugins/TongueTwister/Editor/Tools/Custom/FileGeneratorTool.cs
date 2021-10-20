using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using TongueTwister.Models;
using UnityEditor;
using UnityEngine;

namespace TongueTwister.Editor.Tools.Custom
{
    /// <summary>
    /// Generates various types of files from configured TongueTwister data.
    /// </summary>
    public class FileGeneratorTool : TongueTwisterTool
    {
        private const string LAST_TRANSLATION_FILES_OUTPUT_PATH_PLAYER_PREF = "TT:LastTranslationFilesOutputFolder";

        private const string LAST_DISPLAY_KEY_CONST_FILE_OUTPUT_PATH_PLAYER_REF = "TT:LastDisplayKeyConstFileOutputFile";

        private string _translationFilesOutputFolder, _displayKeyConstOutputFilePath;

        private const string INVALID_C_NAME_START_CHARACTERS = "0123456789";

        private const string DISPLAY_KEY_GEN_LOG_PREFIX = "<b>[DK-CONST-GEN]</b> -";

        public override string Category => "Tools";

        public override string Title => "File Generator Tools";

        public override string Description => "Generate various types of files from configured TongueTwister data.";

        public override string Version => "2.1";
        
        public FileGeneratorTool()
        {
            _translationFilesOutputFolder = 
                EditorPrefs.HasKey(LAST_TRANSLATION_FILES_OUTPUT_PATH_PLAYER_PREF) 
                    ? EditorPrefs.GetString(LAST_TRANSLATION_FILES_OUTPUT_PATH_PLAYER_PREF) 
                    : Application.dataPath;
            
            EditorPrefs.SetString(LAST_TRANSLATION_FILES_OUTPUT_PATH_PLAYER_PREF, _translationFilesOutputFolder);
            
            _translationFilesOutputFolder = 
                EditorPrefs.HasKey(LAST_DISPLAY_KEY_CONST_FILE_OUTPUT_PATH_PLAYER_REF) 
                    ? EditorPrefs.GetString(LAST_DISPLAY_KEY_CONST_FILE_OUTPUT_PATH_PLAYER_REF) 
                    : Application.dataPath;
            
            EditorPrefs.SetString(LAST_DISPLAY_KEY_CONST_FILE_OUTPUT_PATH_PLAYER_REF, _displayKeyConstOutputFilePath);
        }
        
        public override void DrawEditorUi()
        {
            if (GUILayout.Button(
                new GUIContent(
                    "Generate JSON Locale Files", 
                    "Create one JSON locale file for each configured locale, containing all display keys and translations related to it. This is useful for supplying modders with templates for additional translation files.")))
            {
                GenerateJsonLocaleFiles();
            }

            if (GUILayout.Button(
                new GUIContent(
                    "Generate Display Key Constants",
                    "Create a static structure of C# classes containing string constants to represent all configured display keys. This is useful for development and using the DisplayKey attribute on string fields in scripts."))
            )
            {
                GenerateDisplayKeyConstants();
            }
        }

        private void GenerateJsonLocaleFiles()
        {
            var folderPath = EditorUtility.OpenFolderPanel("Choose folder path", _translationFilesOutputFolder, "");
            if (!string.IsNullOrWhiteSpace(folderPath))
            {
                _translationFilesOutputFolder = folderPath;
                if (Directory.Exists(_translationFilesOutputFolder)) EditorPrefs.SetString(LAST_TRANSLATION_FILES_OUTPUT_PATH_PLAYER_PREF, _translationFilesOutputFolder);
                else
                {
                    EditorUtility.DisplayDialog("Error", "Invalid directory - it doesn't exist.", "Close");
                    return;
                }
            }
            else
            {
                return;
            }
            var localizationDictionary = TongueTwisterWindow.LocalizationDictionary;
            var locales = TongueTwisterWindow.CurrentWindow.LocalizationManagerEditor.ConfiguredLocales;
            foreach (var locale in locales)
            {
                var serializedLocalizations = new List<DisplayKeyLocalizationImport>();
                foreach (var displayKey in localizationDictionary.Keys)
                {
                    var localization = localizationDictionary[displayKey][locale];
                    if (localization != null)
                    {
                        serializedLocalizations.Add(new DisplayKeyLocalizationImport(displayKey, localization.Text));
                    }
                }
                var serializedLocalizationSet = new LocaleLocalizationImport()
                {
                    LocaleMetadata = new LocaleMetadata(locale.Metadata),
                    Localizations = serializedLocalizations.ToArray()
                };
                var filePath = Path.Combine(_translationFilesOutputFolder, $"{locale.Metadata.DisplayName ?? locale.Id}.json");
    
                CreateJsonLocalizationFile(serializedLocalizationSet, filePath);
            }
            EditorUtility.RevealInFinder(_translationFilesOutputFolder);
        }

        private void CreateJsonLocalizationFile(LocaleLocalizationImport localeLocalizationImport, string filePath)
        {
            try
            {
                var fileContent = JsonUtility.ToJson(localeLocalizationImport, true);
                File.WriteAllText(filePath, fileContent);
            }
            catch (Exception exception)
            {
                Debug.LogError($"Failed to serialize JSON locale localization set. Reason: {exception.Message}");
            }
        }

        private void GenerateDisplayKeyConstants()
        {
            var fullFilePath = EditorUtility.SaveFilePanel("Choose folder path", _displayKeyConstOutputFilePath, "DisplayKeyConstants", "cs");
            var fileName = "";
            if (!string.IsNullOrWhiteSpace(fullFilePath))
            {
                _displayKeyConstOutputFilePath = fullFilePath;
                fileName = _displayKeyConstOutputFilePath.Split('/').Last();
                var folderPath = _displayKeyConstOutputFilePath.TrimEnd(fileName.ToCharArray());
                if (Directory.Exists(folderPath)) EditorPrefs.SetString(LAST_DISPLAY_KEY_CONST_FILE_OUTPUT_PATH_PLAYER_REF, _displayKeyConstOutputFilePath);
                else
                {
                    EditorUtility.DisplayDialog("Error", "Invalid directory - it doesn't exist.", "Close");
                    return;
                }
            }
            else
            {
                return;
            }

            Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Starting.");

            var fileContent = BuildOutputString(fileName);

            try
            {
                File.WriteAllText(_displayKeyConstOutputFilePath, fileContent);
                EditorUtility.RevealInFinder(_displayKeyConstOutputFilePath);
                AssetDatabase.Refresh();
            }
            catch (Exception exception)
            {
                EditorUtility.DisplayDialog("Error", $"Failed to write file. Reason: {exception.Message}", "Close");
            }
            
            Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Done.");
        }

        private string BuildOutputString(string filename)
        {
            var root = TongueTwisterWindow.CurrentWindow.modelEditorTreeView.tree.Root;

            if (!root.HasChildren)
            {
                EditorUtility.DisplayDialog("Error", "There are no display keys to write.", "Close");
                Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} There are no display keys to write. Finishing.");
                return null;
            }
            
            var baseClassName = 
                Regex.Replace(
                    RemoveFileExtension(filename, ".cs"), 
                    @"[^0-9a-zA-Z:,]+", 
                    "");

            var fileOutput = $"public static class {baseClassName}\n{{\n";

            WriteGroupContent(ref fileOutput, 1, root);

            fileOutput += "}\n";
            
            return fileOutput;
        }

        private string RemoveFileExtension(string fileName, string fileExtension)
        {
            var indexOf = fileName.IndexOf(fileExtension, StringComparison.Ordinal);
            return fileName.Remove(indexOf, fileExtension.Length);
        }

        private string CalculateIndentString(int indent)
        {
            var result = "";

            for (int i = 0; i < indent; i++) result += "\t";

            return result;
        }

        private void WriteDisplayKey(ref string fileOutput, int indent, TongueTwisterModel model)
        {
            if (model.HasErrors)
            {
                Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Display Key {model.Name ?? "UNNAMED"} has errors, skipping.");
                return;
            }
            
            var formattedName = model.FormattedName;

            if (INVALID_C_NAME_START_CHARACTERS.Contains(model.FormattedName[0]))
            {
                Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Display Key {model.Name} has an invalid starting character in its formatted name, \"{formattedName}\", skipping.");
                return;
            }

            fileOutput += CalculateIndentString(indent) + $"public const string {formattedName} = \"{model.GetFullDotName()}\";\n";
        }

        private void WriteGroup(ref string fileOutput, int indent, TongueTwisterModel model)
        {
            if (model.HasErrors)
            {
                Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Group {model.Name ?? "UNNAMED"} has errors, skipping.");
                return;
            }
            
            var formattedName = model.FormattedName;

            if (INVALID_C_NAME_START_CHARACTERS.Contains(model.FormattedName[0]))
            {
                Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Group {model.Name} has an invalid starting character in its formatted name, \"{formattedName}\", skipping.");
                return;
            }

            var calculatedIndentString = CalculateIndentString(indent);
            
            fileOutput += calculatedIndentString + $"public static class {formattedName}\n";
            fileOutput += calculatedIndentString + "{\n";

            WriteGroupContent(ref fileOutput, indent, model);

            fileOutput += calculatedIndentString + "}\n";
        }

        private void WriteGroupContent(ref string fileOutput, int indent, TongueTwisterModel model)
        {
            if (model.HasDisplayKeyChildren)
            {
                var displayKeys = model.Children.Where(child =>child.Type == TongueTwisterModel.ModelType.DisplayKey);
                foreach (var displayKey in displayKeys)
                {
                    if (displayKey.FormattedName == model.FormattedName)
                    {
                        Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Display Key {displayKey.Name} has the same name as parent group {model.Name}, this is not allowed for C# classes, members, fields, etc. Skipping.");
                        continue;
                    }
                    
                    WriteDisplayKey(ref fileOutput, indent + 1, displayKey);
                }
            }

            if (model.HasGroupChildren)
            {
                var groups = model.Children.Where(child =>child.Type == TongueTwisterModel.ModelType.Group);
                foreach (var group in groups)
                {
                    if (group.FormattedName == model.FormattedName)
                    {
                        Debug.Log($"{DISPLAY_KEY_GEN_LOG_PREFIX} Group {group.Name} has the same name as parent group {model.Name}, this is not allowed for C# classes, members, fields, etc. Skipping.");
                        continue;
                    }
                    
                    WriteGroup(ref fileOutput, indent + 1, group);
                }
            }
        }
    }
}